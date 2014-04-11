/// <reference path="../app.js" />
'use strict';

scoreApp.factory('scoreData', function () {
    return {
        getAll: function () {
            return [
                {
                    id: 1,
                    reason: 'É com vocêam Lombardiam. Vem pra lá, mah voceam vai pra cá. Agoram vaimm, agoram vaim. Eu só acreditoammmm.....',
                    date: '12/01/2014 12:10:55',
                    creator: { email: 'a@jo.com', name: 'Fulano', avatar: 'app/img/no_avatar.jpg' },
                    candidate: { email: 'b@jo.com', name: 'Ciclano', avatar: 'app/img/no_avatar.jpg' },
                    voters: [
                        { email: 'b@jo.com', name: 'Ciclano', avatar: 'app/img/no_avatar.jpg', isInFavor: true },
                        { email: 'c@jo.com', name: 'Beltrano', avatar: 'app/img/no_avatar.jpg', isInFavor: false }
                    ]
                },
                {
                    id: 2,
                    reason: 'Ma quem quer dinheiroam? Boca sujuam... sem vergonhuamm. Ma vocêm está cetoam dissoam? O Rauam Gil-uam é gayam! ... Mah Oh Ah Ae Ih Ih o Rauam Gil-uam é gayam!',
                    date: '10/04/2014 12:10:55',
                    creator: { email: 'a@jo.com', name: 'Beltrano', avatar: 'app/img/no_avatar.jpg' },
                    candidate: { email: 'b@jo.com', name: 'Fulano', avatar: 'app/img/no_avatar.jpg' },
                    voters: [
                        { email: 'b@jo.com', name: 'Ciclano', avatar: 'app/img/no_avatar.jpg', isInFavor: true },
                        { email: 'b@jo.com', name: 'Silvio', avatar: 'app/img/no_avatar.jpg', isInFavor: true },
                        { email: 'b@jo.com', name: 'Outro', avatar: 'app/img/no_avatar.jpg', isInFavor: true },
                        { email: 'c@jo.com', name: 'Beltrano', avatar: 'app/img/no_avatar.jpg', isInFavor: false }
                    ]
                }
            ]
        },
        save: function (score) {

        }
    };
});