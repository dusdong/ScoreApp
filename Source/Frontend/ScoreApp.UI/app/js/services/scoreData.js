'use strict';

scoreApp.factory('scoreData', ['$http', '$q', function ($http, $q) {

    return {
        getAll: function (timeUp) {
            var deferred = $q.defer();

            var url = 'api/scores?timeUp=' + timeUp;
            $http.get(url).success(function (data, status) {
                deferred.resolve(data);
            }).error(function (data, status) {
                deferred.reject(data, status);
            })

            return deferred.promise;
        },
        save: function (score) {
            scores.push(score);
        }
    };
}]);