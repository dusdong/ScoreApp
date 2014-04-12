'use strict';

var underscore = angular.module('underscore', [])
    .factory('_', function () {
        return window._;
    });

var scoreApp = angular.module('scoreApp', ['ngRoute', 'angularMoment', 'ui.bootstrap', 'underscore'])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider.when('/Votacao', { templateUrl: '/app/partials/Scores.html', controller: 'ScoreController' });
        $routeProvider.otherwise({ redirectTo: '/Votacao' });
        $locationProvider.html5Mode(true);
    }])
    .run(['$window', function ($window) {
        $window.moment.lang('pt-BR');
    }]);