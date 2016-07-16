(function () {
    'use strict';

    angular.module('ui.grid.breeze', ['ui.grid', 'breeze.angular'])

    .service('uiGridBreezeService', ['breeze', '$parse', function (breeze, $parse) {

        var _schemaGenerator = function (manager, entity, schema) {
            var typeObj = manager.metadataStore.getEntityType(entity);

            typeObj.dataProperties.forEach(function (prop) {
                var proptype = "string";
                if (prop.dataType.isNumeric) {
                    proptype = "number";
                }
                else if (prop.dataType.isDate) {
                    proptype = "date";
                }
                else if (prop.dataType.name == "Boolean") {
                    proptype = "boolean";
                }
                schema.push({ field: prop.name, type: proptype })
            });
        }

        var _makeSchema = function (manager, entity) {
            var schema = [];
            if (manager.metadataStore.isEmpty()) {
                manager.metadataStore.fetchMetadata(manager.serviceName, function () {
                    _schemaGenerator(manager, entity, schema);
                });
            }
            else
                _schemaGenerator(manager, entity, schema);

            return schema;
        }

        this.init = function ($scope, $element, grid) {
            if(grid.options.breeze){
                var manager = grid.options.breeze.manager;
                var entity = grid.options.breeze.entity;
                var resourceName = grid.options.breeze.resourceName;
                var colGeneration = grid.options.breeze.autoColGeneration;

                var methods = {
                    breeze: {
                        changeSource: function (newEntity, newResourceName) {
                            grid.options.columnDefs = _makeSchema(manager, newEntity);
                            grid.options.breeze.entity = newEntity;
                            grid.options.breeze.resourceName = newResourceName;
                            var query = new breeze.EntityQuery()
                            .from(newResourceName);

                            manager.executeQuery(query).then(function (data) {
                                grid.options.data = data.results;
                            });
                        },
                        updateEntities: function(){
                            grid.options.data = manager.getEntities(grid.options.breeze.entity);
                        }
                    }
                }

                grid.api.registerMethodsFromObject(methods);

                if (colGeneration) {
                    grid.options.columnDefs = _makeSchema(manager, entity);
                }

                

                var query = new breeze.EntityQuery()
                    .from(resourceName);

                manager.executeQuery(query).then(function (data) {
                    grid.options.data = data.results;
                });
            }
        };
    }])

    .directive('uiGridBreeze', ['uiGridBreezeService', function (uiGridBreezeService) {
        return {
            restrict: 'A',
            replace: true,
            priority: 10,
            require: '^uiGrid',
            scope: false,
            compile: function () {
                return {
                    pre: function ($scope, $elm, $attrs, uiGridCtrl) {
                        uiGridBreezeService.init($scope, $elm, uiGridCtrl.grid);
                    }
                };
            }
        };
    }])

}());
