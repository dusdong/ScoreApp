'use strict';

scoreApp.controller('LoginController', ['$scope', '$routeParams', '$location', 'authentication', function ($scope, $routeParams, $location, authentication) {

    function redirectToInitialUrl() {
        if ($routeParams.returnUrl) {
            $location.search('returnUrl', null);
            $location.path($routeParams.returnUrl);
        }
        else {
            $location.path('/');
        }
    };

    $scope.login = function () {
        var user = { login: 'JobaDiniz', name: 'Joberto', lastName: 'Diniz' };//TODO: call service to actually login.

        authentication.setCurrentUser(user);
        redirectToInitialUrl();
    };
}]);