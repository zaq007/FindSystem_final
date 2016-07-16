'use strict';

var module = angular.module("FindAdmin", ['breeze.angular', 'ui.grid', 'ui.grid.edit', 'ui.grid.breeze']);

module.controller("AdminController", ['$scope', 'breeze',
    function ($scope, breeze) {
        var manager = new breeze.EntityManager('breeze/AdminEntity');
        
        $scope.setTableSource = function (map) {
            $scope.gridApi.breeze.changeSource(map.entity, map.resourceName);
        }

        $scope.controllersMap = {
            users: { entity: "UserProfile", resourceName: "UserProfiles" },
            tasks: { entity: "Task", resourceName: "Tasks" },
            pathes: { entity: "Path", resourceName: "Pathes" },
            path_task: { entity: "Path_Task", resourceName: "Path_Task" }
        };

        $scope.saveChanges = function () {
            if (manager.hasChanges())
                manager.saveChanges();
        };

        $scope.addItem = function () {
            manager.createEntity($scope.gridOptions.breeze.entity);
            $scope.gridApi.breeze.updateEntities();
        }

        $scope.gridOptions = {
            breeze: {
                manager: manager,
                entity: $scope.controllersMap.users.entity,
                resourceName: $scope.controllersMap.users.resourceName,
                autoColGeneration: true
            }
        };

        $scope.gridOptions.onRegisterApi = function (gridApi) {
            $scope.gridApi = gridApi;
        };
    }]);