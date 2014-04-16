'use strict';

var underscore = angular.module('underscore', [])
    .factory('_', function () {
        return window._;
    });

var scoreApp = angular.module('scoreApp', ['ngRoute', 'angularMoment', 'UserApp', 'ui.bootstrap', 'underscore'])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider.when('/Votacao', { templateUrl: '/app/partials/Voting.html', controller: 'VotingController' });
        $routeProvider.when('/Home', { templateUrl: '/app/partials/Home.html', public: true });
        $routeProvider.when('/Login', { templateUrl: '/app/partials/Login.html', login: true });
        $routeProvider.otherwise({ redirectTo: '/Home' });
        $locationProvider.html5Mode(true);
    }])
    .run(['$window', '$rootScope', '$location', 'user', function ($window, $rootScope, $location, user) {
        $window.moment.lang('pt-BR');
        user.init({
            appId: '534db88a7c34a'
        });
    }]);