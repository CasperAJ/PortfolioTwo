define(["jquery", "knockout", 'dataService', 'postman'], function($, ko, ds, postman) {
    return function(params){
        var markings = ko.observableArray();
        
        ds.GetMarks(function(data) {
            markings(data.data);
        });

        var goToPost = function(data){
            postman.publish("currentPostChanged", data.post);
            postman.publish("changeComponent", 'post');
        }


        return {
            markings,
            goToPost
        };
    };
});