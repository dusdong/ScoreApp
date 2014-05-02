'use strict';

scoreApp.controller('VotingController', ['$scope', 'scoreData', 'toaster', 'user', function ($scope, scoreData, toaster, user) {
    var promise = scoreData.getAll(false);
    $scope.scoresPromise = promise;
    promise.then(function (data) {
        $scope.scores = data;
    }, function (data) {
        toaster.pop('error', '', 'Sorry, but an error occured');
    });

    $scope.candidateIsLoggedUser = function (candidate) {
        return candidate.id == user.current.user_id;
    };

    $scope.vote = function (scoreId, vote) {

    };
}]);