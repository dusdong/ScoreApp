'use strict';

var underscore = angular.module('underscore', [])
    .factory('_', function () {
        return window._;
    });

var scoreApp = angular.module('scoreApp', ['ngRoute', 'angularMoment', 'ui.bootstrap', 'underscore'])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider.when('/Votacao', { templateUrl: '/app/partials/Voting.html', controller: 'VotingController' });
        $routeProvider.when('/Login', { templateUrl: '/app/partials/Login.html', controller: 'LoginController' });
        $routeProvider.otherwise({ redirectTo: '/Votacao' });
        $locationProvider.html5Mode(true);
    }])
    .run(['$window', '$rootScope', '$location', 'authentication', function ($window, $rootScope, $location, authentication) {
        $window.moment.lang('pt-BR');

        $rootScope.$on('$routeChangeStart', function (event, next, current) {
            if (!authentication.isLoggedIn()) {
                if (next.templateUrl != "/app/partials/Login.html") {
                    var returnUrl = $location.url();
                    event.preventDefault();
                    $location.path('/Login').search({ returnUrl: returnUrl });
                }
            }
        });
    }]);