import Oidc, { UserManager } from 'oidc-client'
import config from './config'
import router from '../routing/router'

if (localStorage.getItem('oicdLog')) {
    Oidc.Log.logger = console;
}

class AuthService {

    constructor() {
        this.userManager = new UserManager(config);
        this.user = {
            loggedIn: false,
            name: null,
            id: null
        };

        this.silentLogin();
    }

    silentLogin() {
        console.log("starting singinSilent");
        return this.userManager.signinSilent()
            .then(user => {
                this.applyUser(user)
                console.log("singinSilent success");
                return user;
            })
            .catch(error => {
                if (error.error === 'login_required') {
                    console.info('Skipping silent login - authentication required');
                    return;
                }

                console.log("silent error");

                console.error(error);
            });

        console.log("silent done");
    }

    login() {
        if (!isLocalStorageSupported()) {
            console.error('Anmeldung nicht möglich', 'Bitte verlasse den privaten Modus, um die Anmeldung zu nutzen.');
            return;
        }

        this.userManager.signinRedirect()
            .catch(error => {
                console.error(error);
                console.error('Fehler bei der Anmeldung', 'Die Anmeldung ist leider nicht verfügbar. Wir werden das Problem so schnell wie möglich beheben.');
            });
    }

    logout() {
        this.userManager.signoutRedirect()
            .catch(error => {
                console.error(error);
                console.error('Fehler bei der Abmeldung', 'Wir werden das Problem so schnell wie möglich beheben.');
            });
    }

    completeLogin() {
        this.userManager.signinRedirectCallback()
            .then(user => {
                const path = localStorage.getItem('loginRedirect') || '/';

                localStorage.removeItem('loginRedirect');
                this.applyUser(user);
                router.push(path);
            })
            .catch(error => {
                console.error(error);
                router.push('/');
            });
    }

    applyUser(user) {
        if (user) {
            console.log("apply user");
            console.log(user)
            this.user.loggedIn = true;
            this.user.name = user.profile.name;
            this.user.id = user.profile.sub;

            window.localStorage.setItem('access_token', user.access_token);
            console.info('Access Token: ' + user.access_token);

            return;
        }

        this.user.loggedIn = false;
        this.user.name = null;
        this.user.id = null;
    }

    getUser() {
        return this.userManager.getUser();
    }

    getToken() {
        return this.userManager.getUser()
            .then(user => {
                if (!user) {
                    return '';
                }

                return user.access_token;
            });
    }

    getUserId() {
        return this.getUser()
            .then(user => {
                if (!user) {
                    return null;
                }

                return user.profile.sub;
            });
    }

}

export default new AuthService();

function isLocalStorageSupported() {
    try {
        localStorage.setItem('test', 'test');
    } catch (error) {
        console.warn(error);
        return false;
    }

    return true;
}
