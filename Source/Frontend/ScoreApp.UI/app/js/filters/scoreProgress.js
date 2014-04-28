"use strict";

scoreApp.filter('scoreProgress', ['scoreCalculator', function (scoreCalculator) {
    return function (score) {
        return scoreCalculator.percentage(score);
    }
}]);