import axios from 'axios'

axios.interceptors.request.use(function(config) {
    const token = window.localStorage.getItem('access_token')
    if (token) {
        console.log("applying token");
        config.headers.Authorization = `Bearer ${token}`
    } else {
        console.log("no token");
    }

    return config
})


class httpService {
    constructor() {
        this.apiPath = "https://api.kidsradar.de";
    }
}


export default new httpService();