'use strict';

var module = angular.module("findApp", ['ngRoute', 'ui.bootstrap']);

module.controller("mainController", ['$scope', '$http',
    function ($scope, $http) {
        $scope.gameStarted = false;
        $scope.currentTeamId = 0;
        $scope.message = null;
        $scope.img = null;
        $scope.task = null;
        $scope.rotated = false;
        $scope.frozen = false;
        $scope.end = false;
        $scope.scoreboard = [];
        $scope.points = 0;

        $scope.startGame = function () {
            $http.post('/Game/Start').then(function(data)
            {
                $scope.gameStarted = true;
                $scope.img = data.data[0].Url;
                $scope.task = data.data[0].Description;
                $scope.number = data.data[0].number;
            });
        }

        $scope.tryAnswer = function (input) {
            $scope.message = null;
            $http.post('/Game/TryAnswer', { "answer": input }).then(function (data) {
                console.log(data);
                if (data.data.end === true) {
                    $scope.end = true;
                    $scope.message = "End of the game";
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

        $scope.freezeTeam = function (teamId) {
            console.log(teamId);
            $http.get('/Game/Freeze?userId=' + teamId, { "userId": teamId }).then(function (data) {
                alert(data.data.data);
            });
        }

        $http.get('/Game/State').then(function (data) {
            $scope.gameStarted = data.data.gameStarted || false;
            $scope.currentTeamId = data.data.teamId;
            $scope.points = data.data.points;
            $scope.img = data.data.img;
            $scope.task = data.data.comments;
            $scope.number = data.data.number;
            $scope.rotated = data.data.rotated || false;
            $scope.frozen = data.data.frozen || false;
            $scope.scoreboard = JSON.parse(data.data.scoreboard) || [];
            $scope.end = data.data.end;
            if($scope.end)
                $scope.message = "End of the game";
        });


        var chat = $.connection.findHub;          
        chat.client.setFrozen = function () {
            $scope.frozen = true;
        };

        chat.client.setUnfrozen = function () {
            $scope.frozen = false;
        };

        chat.client.scoreboard = function (teamId, pos) {
            for (var i = 0; i < $scope.scoreboard.length; i++)
                if ($scope.scoreboard[i].teamId == teamId) {
                    $scope.scoreboard[i].position = pos;
                }
        };

        $.connection.hub.start();
        
    }]);