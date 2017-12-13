import axios from 'axios'
import httpService from './httpService'

const apiService = {
    getStatus() {
        return axios.get(httpService.apiPath + '/values/5')
            .then(response => {
                return response;
            })
    }
}

export default apiService