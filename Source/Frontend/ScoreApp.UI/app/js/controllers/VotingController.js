'use strict';

scoreApp.controller('VotingController', ['$scope', 'scoreData', 'toaster', 'user', 'underscore', function ($scope, scoreData, toaster, user, _) {
    var promise = scoreData.getAll(false);
    $scope.promise = promise;
    promise.then(function (data) {
        $scope.scores = data;
    }, function (data) {
        toaster.pop('error', '', 'Sorry, but an error occured');
    });

    $scope.candidateIsLoggedUser = function (candidate) {
        return candidate.id == user.current.user_id;
    };
    $scope.loggedUserVote = function (score) {
        var userVoter = _.find(score.voters, function (voter) {
            return voter.id == user.current.user_id;
        });
        if (userVoter == null)
            return 'default';
        return userVoter.isInFavor;
    };
    $scope.vote = function (scoreId, vote) {

    };
}]);