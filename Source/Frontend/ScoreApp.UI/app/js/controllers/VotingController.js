'use strict';

scoreApp.controller('VotingController',
    function VotingController($scope, scoreData) {
        $scope.scores = scoreData.getAll();
        $scope.vote = function (scoreId, vote) {

        };
    });