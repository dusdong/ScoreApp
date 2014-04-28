"use strict";

scoreApp.filter('voterColor', function () {
    return function (voter) {
        return voter.isInFavor ? 'green' : 'red';
    }
});