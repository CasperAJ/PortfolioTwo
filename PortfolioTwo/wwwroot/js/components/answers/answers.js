define(["jquery", "knockout", 'dataService', 'postman', 'app'], function($, ko, ds, postman, vm) {
    return function(params){
        var answers = ko.observableArray();


        ds.GetAnswers(function(data) {
            answers(data);

        });

        return {
            answers
        };
    };
});