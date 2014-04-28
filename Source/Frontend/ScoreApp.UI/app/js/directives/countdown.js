"use strict";

scoreApp.directive('countdown', function () {
    return {
        restrict: 'E',
        template: '<div></div>',
        replace: true,
        scope: {
            secondsToExpire: '&'
        },
        link: function (scope, element, attrs) {
            var date = moment().add('seconds', scope.secondsToExpire()).toDate();
            $(element).countdown({
                date: date,
                render: function (data) {
                    $(this.el).text(this.leadingZeros(data.min, 2) + 'm ' + this.leadingZeros(data.sec, 2) + 's');
                }
            });
        }
    };
});