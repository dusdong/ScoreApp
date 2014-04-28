'use strict';

var underscore = angular.module('underscore', [])
    .factory('underscore', function () {
        return window._;
    });

var scoreApp = angular.module('scoreApp', ['ngRoute', 'ngAnimate', 'angularMoment', 'UserApp', 'UserApp.facebook-picture', 'ui.bootstrap', 'underscore', 'toaster', 'cgBusy'])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider.when('/', { templateUrl: '/app/partials/Home.html', public: true });
        $routeProvider.when('/Votacao', { templateUrl: '/app/partials/Voting.html', controller: 'VotingController' });
        $routeProvider.when('/Login', { templateUrl: '/app/partials/Login.html', login: true });
        $routeProvider.otherwise({ redirectTo: '/' });
        $locationProvider.html5Mode(true);
    }])
    .value('cgBusyDefaults', {
        message: 'Awesome content is being fetched...'
    })
    .run(['$window', '$rootScope', '$location', 'user', function ($window, $rootScope, $location, user) {
        $window.moment.lang('pt-BR');
        user.init({
            appId: '534db88a7c34a',
            heartbeatInterval: 0
        });
    }]);