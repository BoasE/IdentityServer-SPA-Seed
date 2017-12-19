using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer.MongoDb;
using IdentityServer.MongoDb.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tavis;

namespace IdentityServer.Administration.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly MongoUserStore _userStore;

        public UsersController(MongoUserStore userStore)
        {
            _userStore = userStore;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return await Task.FromResult(new OkObjectResult(_userStore.FindByUsername("alex")));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]UserDto user)
        {
            var hasher = new PasswordHasher<MongoExternalUser>();
            try
            {
                var mongoExternalUser = await _userStore.AutoProvisionUser("IdSrv", user.Username, new List<Claim>
                {
                    new Claim(JwtClaimTypes.Name, user.Username),
                    new Claim(JwtClaimTypes.Email, user.Email)
                });
                var hash = hasher.HashPassword(mongoExternalUser, user.Password);
                await _userStore.SetPasswordHashForUser(mongoExternalUser, hash);
                return await Task.FromResult(new OkResult());
            }
            catch (Exception e)
            {
                if (e is UserExistsException)
                {
                    var problem = new ProblemDocument
                    {
                        ProblemType = new Uri("http://tempuri.org/errors/user-already-exists"),
                        Title = $"User {user.Username} already exists",
                        StatusCode = HttpStatusCode.BadRequest
                    };

                    var contentResult = new ContentResult();
                    contentResult.Content = await new ProblemContent(problem).ReadAsStringAsync();
                    contentResult.ContentType = "application/problem+json";
                    return await Task.FromResult(contentResult);
                }

                return await Task.FromResult(new StatusCodeResult(500));
            }
        }
    }

    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}