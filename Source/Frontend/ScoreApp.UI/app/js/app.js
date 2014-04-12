'use strict';

var underscore = angular.module('underscore', [])
    .factory('_', function () {
        return window._;
    });

var scoreApp = angular.module('scoreApp', ['ngRoute', 'angularMoment', 'ui.bootstrap', 'underscore'])
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/Pontos', { templateUrl: '/app/partials/Scores.html', controller: 'ScoreController' });
        $routeProvider.otherwise({ redirectTo: '/Pontos' });
    }])
    .run(['$window', function ($window) {
        $window.moment.lang('pt-BR');
    }]);