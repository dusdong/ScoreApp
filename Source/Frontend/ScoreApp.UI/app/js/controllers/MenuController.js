'use strict';

scoreApp.controller('MenuController', ['$scope', 'scoreData', function ($scope, scoreData) {
    scoreData.getAll(false).then(function (data) {
        $scope.scoresToVote = data.length;
    });
}]);