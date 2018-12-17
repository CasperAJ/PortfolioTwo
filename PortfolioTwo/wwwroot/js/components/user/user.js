define(["jquery", "knockout", 'dataService', 'postman'], function($, ko, ds, postman) {
    return function(params){
        var username = ko.observable();
        var password = ko.observable();
        var email = ko.observable();

        var create = function(){
            ds.createUser(function(data){
            }, username(), password(), email());
        }

        var goToLogin = function(){
            postman.publish("changeUserComponent", "login");
        }

        return {
          username,
          password,
          email,
          create,
          goToLogin
        };
    };
});