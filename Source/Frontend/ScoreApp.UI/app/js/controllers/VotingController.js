'use strict';

scoreApp.controller('VotingController', ['$scope', 'scoreData', function ($scope, scoreData) {
    $scope.scores = scoreData.getAll();
    $scope.vote = function (scoreId, vote) {

    };
}]);