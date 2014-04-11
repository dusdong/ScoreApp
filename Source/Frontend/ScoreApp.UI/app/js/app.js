'use strict';

var scoreApp = angular.module('scoreApp', ['angularMoment']);

scoreApp.run(['$window',
    function ($window) {
        $window.moment.lang('pt-BR');
    }
]);