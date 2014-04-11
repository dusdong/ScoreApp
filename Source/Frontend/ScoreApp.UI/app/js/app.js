'use strict';

var underscore = angular.module('underscore', []);
underscore.factory('_', function () {
    return window._;
});

var scoreApp = angular.module('scoreApp', ['angularMoment', 'ui.bootstrap', 'underscore']);

scoreApp.run(['$window',
    function ($window) {
        $window.moment.lang('pt-BR');
    }
]);