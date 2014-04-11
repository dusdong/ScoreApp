'use strict';

var scoreApp = angular.module('scoreApp', ['angularMoment', 'ui.bootstrap']);

scoreApp.run(['$window',
    function ($window) {
        $window.moment.lang('pt-BR');
    }
]);