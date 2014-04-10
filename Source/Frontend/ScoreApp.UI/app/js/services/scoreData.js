/// <reference path="../app.js" />
'use strict';

scoreApp.factory('scoreData', function () {
    return {
        getScores: function () {
            return [
                {
                    id: 1,
                    reason: 'blablablabla',
                    date: '12/01/2014',
                    creator: { email: 'jo@jo.com', name: 'Joba' },
                    candidate: { email: 'jo@jo.com', name: 'tiego' }
                }
            ]
        }
    };
});