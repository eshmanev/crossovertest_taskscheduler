var app = angular.module('scheduler', ['ui.bootstrap', 'ngResource', 'ngRoute']);

// normally it should be replaced with pager which dynamically loads portions of informations
var PageSize = 1000;

app.factory('Clients', [
    '$resource', function($resource) {
        return $resource('/api/clients/:id', null, {
            getList: { method: 'GET', url: '/api/clients?start=:start&count=:count', isArray: true },
        });
    }
]);

app.factory('Commands', [
    '$resource', function ($resource) {
        return $resource('/api/commands/:id', null, {
            getList: { method: 'GET', url: '/api/commands?start=:start&count=:count', isArray: true },
            execute: { method: 'POST', url: '/api/commands/:id/result', isArray: false },
            schedule: { method: 'POST', url: '/api/commands/:id/scheduled', isArray: false }
        });
    }
]);

app.factory('Logs', [
    '$resource', function ($resource) {
        return $resource('/api/logs/:id', null, {
            getList: { method: 'GET', url: '/api/logs?start=:start&count=:count', isArray: true },
        });
    }
]);

app.controller('ClientListController', [
    '$modal', 'Clients', function ($modal, Clients) {
        this.clients = [];

        Clients.getList({ start: 0, count: PageSize }, function (data) { this.clients = data; }.bind(this));

        this.addClient = function () {
            var list = this.clients;
            var client = {};
            $modal.open({
                templateUrl: 'edit-client.html',
                controller: 'ClientEditController',
                resolve: {
                    $list: function() { return list; },
                    $client: function () { return client; }
                }
            });
        }

        this.editClient = function(index) {
            
        }

        this.removeClient = function(index) {
            
        }
    }
]);

app.controller('ClientEditController', [
    '$scope', '$modalInstance', '$client', '$list', 'Clients', function ($scope, $modalInstance, $client, $list, Clients) {
        $scope.client = $client;
        if (typeof $client.id === "undefined")
            $scope.title = 'Add new client';
        else
            $scope.title = 'Edit client ' + $client.name;

        $scope.submit = function() {
            Clients.save($scope.client,
                function (response) {
                    $scope.client.clientId = response.clientId;
                    $list.push($scope.client);
                    $scope.close();
                }.bind(this),
                function (error) {
                    this.errors = error.data.details;
                }.bind(this));

        }

        $scope.close = function () {
            $modalInstance.dismiss('cancel');
        }
    }
]);

app.controller('CommandListController', [
    '$modal', 'Commands', 'Clients', function($modal, Commands, Clients) {
        this.commands = [];

        Commands.getList({ start: 0, count: PageSize }, function(data) { this.commands = data; }.bind(this));

        this.addCommand = function() {
            var list = this.commands;
            var command = {};
            $modal.open({
                templateUrl: 'edit-command.html',
                controller: 'CommandEditController',
                resolve: {
                    $list: function() { return list; },
                    $command: function() { return command; }
                }
            });
        }

        this.editCommand = function(index) {

        }

        this.removeCommand = function(index) {

        }

        this.runCommand = function(index) {
            var command = this.commands[index];
            $modal.open({
                templateUrl: 'run-command.html',
                controller: 'CommandExecutionController',
                resolve: {
                    $command: function() { return command; }
                }
            });
        }

        this.schedule = function(index) {
            var command = this.commands[index];
            $modal.open({
                templateUrl: 'edit-scheduler.html',
                controller: 'SchedulerController',
                resolve: {
                    $command: function() { return command; }
                }
            });
        }
    }
]);

app.controller('SchedulerController', [
    '$scope', '$modalInstance', '$command', 'Commands', function($scope, $modalInstance, $command, Commands) {
        $scope.title = "Schedule command execution";
        var time = new Date();
        time.setHours(12);
        time.setMinutes(0);
        $scope.scheduler = {time: time};
        $scope.submit = function() {
            Commands.schedule({ id: $command.commandId }, $scope.scheduler,
                function(response) {
                    $scope.output = response.commandOutput;
                }.bind(this),
                function(error) {
                    $scope.errors = error.data.details;
                    $scope.output = error.data.message;
                });
        }

        $scope.close = function() {
            $modalInstance.dismiss('cancel');
        }

        $scope.open = function($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.opened = true;
        };
    }
]);

app.controller('CommandExecutionController', [
    '$scope', '$modalInstance', '$command', 'Commands', function ($scope, $modalInstance, $command, Commands) {
        $scope.command = $command;
        $scope.title = 'Running command "' +  $command.commandText + '" on "' + $command.clientName + '"...';
        $scope.output = "Running. Please wait for a while...";

        Commands.execute({ id: $command.commandId }, {},
                function (response) {
                    $scope.output = response.commandOutput;
                }.bind(this),
                function (error) {
                    $scope.errors = error.data.details;
                    $scope.output = error.data.message;
                });

        $scope.close = function () {
            $modalInstance.dismiss('cancel');
        }
    }
]);

app.controller('CommandEditController', [
    '$scope', '$modalInstance', '$command', '$list', 'Commands', 'Clients', function ($scope, $modalInstance, $command, $list, Commands, Clients) {
        $scope.command = $command;
        $scope.availableClients = [];
        Clients.getList({ start: 0, count: PageSize }, function (data) { $scope.availableClients = data; }.bind(this));

        if (typeof $command.id === "undefined")
            $scope.title = 'Add new command';
        else
            $scope.title = 'Edit command';

        $scope.submit = function () {
            Commands.save($scope.command,
                function (response) {
                    $scope.command.commandId = response.commandId;
                    $list.push($scope.command);
                    $scope.close();
                }.bind(this),
                function (error) {
                    this.errors = error.data.details;
                }.bind(this));

        }

        $scope.close = function () {
            $modalInstance.dismiss('cancel');
        }
    }
]);