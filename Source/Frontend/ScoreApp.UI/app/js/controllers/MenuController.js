'use strict';

scoreApp.controller('MenuController', ['$scope', 'scoreData', function ($scope, scoreData) {
    $scope.scoresToVote = scoreData.count();
}]);