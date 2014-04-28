'use strict';

scoreApp.controller('VotingController', ['$scope', 'scoreData', 'toaster', function ($scope, scoreData, toaster) {
    var promise = scoreData.getAll(false);
    $scope.scoresPromise = promise;
    promise.then(function (data) {
        $scope.scores = data;
    }, function (data) {
        toaster.pop('error', '', 'Sorry, but an error occured');
    });

    $scope.vote = function (scoreId, vote) {

    };
}]);