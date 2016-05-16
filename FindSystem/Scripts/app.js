'use strict';

var module = angular.module("findApp", ['ngRoute', 'ui.bootstrap']);

module.controller("mainController", ['$scope', '$http',
    function ($scope, $http) {
        $scope.gameStarted = false;
        $scope.message = null;
        $scope.img = null;
        $scope.task = null;
        $scope.rotated = false;
        $scope.frozen = false;
        $scope.end = false;
        $scope.scoreboard = [];
        $scope.startGame = function () {
            $http.post('/Game/Start').then(function(data)
            {
                $scope.gameStarted = true;
                $scope.img = data.data[0].img;
                $scope.task = data.data[0].comments;
                $scope.number = data.data[0].number;
            });
        }

        $scope.tryAnswer = function (input) {
            $scope.message = null;
            $http.post('/Game/TryAnswer', { "answer": input }).then(function (data) {
                console.log(data);
                if (data.data.end === true) {
                    $scope.end = true;
                } else
                    if (data.data.right === true) {
                        $scope.img = data.data.img;
                        $scope.task = data.data.comments;
                        $scope.number = data.data.number;
                    } else {
                        $scope.message = data.data.message;
                    }
                
            });
        }

        $http.get('/Game/State').then(function (data) {
            $scope.gameStarted = data.data[0].gameStarted || false;
            $scope.img = data.data[0].img;
            $scope.task = data.data[0].comments;
            $scope.number = data.data[0].number;
            $scope.rotated = data.data[0].rotated || false;
            $scope.frozen = data.data[0].frozen || false;
            $scope.scoreboard = JSON.parse(data.data[0].scoreboard) || [];
        });

        var chat = $.connection.findHub;          
        chat.client.setFrozen = function () {
            $scope.frozen = true;
        };

        chat.client.setUnfrozen = function () {
            $scope.frozen = false;
        };

        $.connection.hub.start();
        
    }]);