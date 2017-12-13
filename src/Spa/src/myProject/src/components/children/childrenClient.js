import axios from 'axios'
import httpService from './httpService'

const apiService = {
    getChildren() {
        return axios.get('https://api.kidsradar.de/children')
            .then(response => {
                return response;
            })
    },


}

export default apiService