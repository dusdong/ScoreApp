'use strict';

scoreApp.controller('MenuController',
    function MenuController($scope, scoreData) {
        $scope.scoresToVote = scoreData.count();
    });