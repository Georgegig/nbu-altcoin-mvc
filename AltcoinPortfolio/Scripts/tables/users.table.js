'use strict';

let UsersTable = (function () {
    let getCookie = function (name) {
        var dc = document.cookie;
        var prefix = name + "=";
        var begin = dc.indexOf("; " + prefix);
        if (begin == -1) {
            begin = dc.indexOf(prefix);
            if (begin != 0) return null;
        }
        else {
            begin += 2;
            var end = document.cookie.indexOf(";", begin);
            if (end == -1) {
                end = dc.length;
            }
        }

        return decodeURI(dc.substring(begin + prefix.length, end));
    };
    
    let userLoggedIn = () => {
        return userAuthorized;
    };

    let loginUser = (email) => {
        let loginDate = new Date();
        let user = {
            email: email,
            timeStamp: loginDate.getFullYear() + '-' + (loginDate.getMonth() + 1) + '-' + loginDate.getDate()
        };
        localStorage.setItem('user', JSON.stringify(user));
    };

    let getLoggedUserMail = () => {
        return JSON.parse(localStorage.getItem('user')).email;
    };

    let logoutUser = () => {
        localStorage.removeItem('user');
    };

    return {
        userLoggedIn: userLoggedIn,
        loginUser: loginUser,
        getLoggedUserMail: getLoggedUserMail,
        logoutUser: logoutUser
    }
})();