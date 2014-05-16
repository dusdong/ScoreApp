'use strict';

scoreApp.controller('MenuController', ['$scope', 'scoreData', 'user', function ($scope, scoreData, user) {
    if (user.authenticated) {
        scoreData.getAll(false).then(function (data) {
            $scope.scoresToVote = data.length;
        });
    }
}]);