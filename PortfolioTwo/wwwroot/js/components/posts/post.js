define(["jquery", "knockout", 'dataService', 'postman', 'app'], function($, ko, ds, postman, vm) {
    return function(params){
        var answers = ko.observableArray([]);
        var singlePost = ko.observable();

        ds.getSinglePost(function(data) {
            console.log(data);
            singlePost(data);
        });
            

        var ChangeLayout = function(){
            postman.publish("changeComponent");
        };


        return {
            singlePost,
            answers,
            ChangeLayout  
        };
    };
});