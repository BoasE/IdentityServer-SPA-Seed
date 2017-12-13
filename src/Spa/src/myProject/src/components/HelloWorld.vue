<template>
  <div class="hello">
    <h1>{{ msg }}</h1>

    <a href="" class="nav-item nav-link" @click.prevent="login" v-if="!user.loggedIn">
                        <i class="fa fa-sign-in fa-fw"></i>
                        Anmelden
                    </a>

                    {{user.id}}{{user.name}};


    <a href="" class="nav-item nav-link" @click.prevent="getStatus">
                       Status
                    </a>

                    {{user.id}};
  </div>

</template>

<script>
import authService from '../auth/authService'
import apiService from './api/apiService'

export default {
  name: 'HelloWorld',
  data () {
    return {
      user: authService.user,
      loggedi:authService.user.loggedIn,
      msg: 'Welcome to Your Vue.js App'
    }
  },
  methods:{
    getStatus(){
      apiService.getStatus().then(data=>{
        console.log(data);
      })
    }    ,
     login() {

                localStorage.setItem('loginRedirect', this.$route.fullPath);
                authService.login();
            },
  }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #42b983;
}
</style>
