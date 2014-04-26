'use strict';

scoreApp.factory('scoreCalculator', ['underscore', function (_) {
    return {
        percentage: function (score) {
            var votersInFavor = _.filter(score.voters, function (voter) {
                return voter.isInFavor == true;
            });

            var percentage = (100 * ((votersInFavor.length * 100) / score.witnesses)) / 50; //50 é quantos % deve haver de votos a favor para que o ponto seja contabilizado (parametrizar).
            return percentage > 100 ? 100 : percentage;
        }
    };
}]);