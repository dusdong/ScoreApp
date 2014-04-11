"use strict";

scoreApp.filter('scoreProgress', function (scoreCalculator) {
    return function (score) {
        return scoreCalculator.percentage(score);
    }
});

scoreApp.filter('progressType', function (scoreCalculator) {
    return function (score) {
        var percentage = scoreCalculator.percentage(score);
        if (percentage < 25)
            return 'sucess';
        if (percentage < 50)
            return 'info';
        if (percentage < 75)
            return 'warning';

        return 'danger';
    }
});