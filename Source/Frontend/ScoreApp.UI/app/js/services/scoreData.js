/// <reference path="../app.js" />
'use strict';

scoreApp.factory('scoreData', function () {
    return {
        getAll: function () {
            return [
                {
                    id: 1,
                    reason: 'É com vocêam Lombardiam. Vem pra lá, mah voceam vai pra cá. Agoram vaimm, agoram vaim. Eu só acreditoammmm.....',
                    date: '2014/04/10 23:10:55',
                    creator: { email: 'a@jo.com', name: 'Fulano', avatar: 'app/img/no_avatar.jpg' },
                    candidate: { email: 'b@jo.com', name: 'Ciclano', avatar: 'app/img/no_avatar.jpg' },
                    witnesses: 4,
                    voters: [
                        { email: 'b@jo.com', name: 'Ciclano', avatar: 'app/img/no_avatar.jpg', isInFavor: true },
                        { email: 'c@jo.com', name: 'Beltrano', avatar: 'app/img/no_avatar.jpg', isInFavor: false }
                    ]
                },
                {
                    id: 2,
                    reason: 'Ma quem quer dinheiroam? Boca sujuam... sem vergonhuamm. Ma vocêm está cetoam dissoam? O Rauam Gil-uam é gayam! ... Mah Oh Ah Ae Ih Ih o Rauam Gil-uam é gayam!',
                    date: '2014/01/01 12:10:55',
                    creator: { email: 'a@jo.com', name: 'Beltrano', avatar: 'app/img/no_avatar.jpg' },
                    candidate: { email: 'b@jo.com', name: 'Fulano', avatar: 'app/img/no_avatar.jpg' },
                    witnesses: 5,
                    voters: [
                        { email: 'b@jo.com', name: 'Ciclano', avatar: 'app/img/no_avatar.jpg', isInFavor: true },
                        { email: 'b@jo.com', name: 'Silvio', avatar: 'app/img/no_avatar.jpg', isInFavor: false },
                        { email: 'b@jo.com', name: 'Outro', avatar: 'app/img/no_avatar.jpg', isInFavor: false },
                        { email: 'c@jo.com', name: 'Beltrano', avatar: 'app/img/no_avatar.jpg', isInFavor: false }
                    ]
                },
                {
                    id: 3,
                    reason: 'Silvio Santos Ipsum mah roda a roduamm. É por suam contam em riscoamm? Um, doisam trêsam, quatruam, PIM, entendeuam? Mah é a portuam da esperançuam. Estamoans em ritmam de festam. É com vocêam.',
                    date: '2014/04/11 11:02:55',
                    creator: { email: 'a@jo.com', name: 'Outro', avatar: 'app/img/no_avatar.jpg' },
                    candidate: { email: 'b@jo.com', name: 'Beltrano', avatar: 'app/img/no_avatar.jpg' },
                    witnesses: 2,
                    voters: [
                        { email: 'b@jo.com', name: 'Outro', avatar: 'app/img/no_avatar.jpg', isInFavor: true },
                        { email: 'b@jo.com', name: 'Fulano', avatar: 'app/img/no_avatar.jpg', isInFavor: true }
                    ]
                },
                {
                    id: 4,
                    reason: 'Silvio Santos Ipsum eu não queriam perguntaram issoam publicamenteam, ma.',
                    date: '2014/04/11 15:02:55',
                    creator: { email: 'a@jo.com', name: 'Laerte', avatar: 'app/img/no_avatar.jpg' },
                    candidate: { email: 'b@jo.com', name: 'Silvio Santos', avatar: 'app/img/no_avatar.jpg' },
                    witnesses: 9,
                    voters: [
                        { email: 'b@jo.com', name: 'Outro', avatar: 'app/img/no_avatar.jpg', isInFavor: true },
                        { email: 'b@jo.com', name: 'Fulano', avatar: 'app/img/no_avatar.jpg', isInFavor: false }
                    ]
                }
            ]
        },
        save: function (score) {

        }
    };
});