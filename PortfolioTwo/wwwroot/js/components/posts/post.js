define(["jquery", "knockout", 'dataService', 'postman', 'app'], function($, ko, ds, postman, vm) {
    return function(params){
        var answers = ko.observableArray([]);
        var singlePost = ko.observable();

        var GetAnswers = ds.GetAnswers(function(data) {
            answers(data);
        });

        ds.getSinglePost(function(data) {
            singlePost(data);
            GetAnswers();
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