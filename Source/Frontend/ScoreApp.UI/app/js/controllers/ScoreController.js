/// <reference path="../app.js" />
'use strict';

scoreApp.controller('ScoreController',
    function ScoreController($scope, scoreData) {
        $scope.scores = scoreData.getScores();
    });