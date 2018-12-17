define(["jquery", "knockout", 'dataService', 'postman'], function($, ko, ds, postman) {
    return function(params){
        var username = ko.observable();
        var password = ko.observable();
        var loginstate = ko.observable(true);

        var authenticate = function(){
            ds.authenticate(function(data){
                postman.publish("tokenUpdated", data.token)
                loginstate(false);
            }, username(), password());
        };

        var logOut = function(){
            postman.publish("tokenUpdated", "");
            loginstate(true);
        }

        var createUser = function(){
            postman.publish("changeUserComponent", "user");
        }

        return {
          authenticate,
          username,
          password,
          loginstate,
          logOut,
          createUser
        };
    };
});