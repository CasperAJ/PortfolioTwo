define(["jquery", "knockout", 'dataService', 'postman', 'app'], function($, ko, ds, postman, vm) {
    return function(params){
        var singlePost = ko.observable();

        

        ds.getSinglePost(function(data) {
            singlePost(data);
        });

        var ChangeLayout = function(){
            postman.publish("changeComponent");
        };


        return {
            singlePost,
            ChangeLayout  
        };
    };
});