"use strict";

scoreApp.factory('authentication', function () {
    var currentUser;

    return {
        isLoggedIn: function () {
            return (currentUser) ? true : false;
        },
        setCurrentUser: function (user) {
            currentUser = user;
        },
        getCurrentUser: function () {
            return currentUser;
        }
    };
});