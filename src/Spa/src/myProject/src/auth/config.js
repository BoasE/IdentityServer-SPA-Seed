const domain = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '')

export default {
    authority: 'http://localhost/',
    client_id: 'vac',
    redirect_uri: `${domain}/auth`,
    response_type: 'id_token token',
    scope: 'openid offline_access email profile lessons',
    post_logout_redirect_uri: domain,
    automaticSilentRenew: true,
    silent_redirect_uri: `${domain}/silent.html`,
    client_secret: 'vacsecretdfgfd333'
}
