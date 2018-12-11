define(["jquery", "knockout", 'dataService'], function($, ko, ds) {
    return function(params){
        var comments = ko.observableArray();

        ds.GetComments(function(data) {
            comments(data.data);
        }, params.url);

        return {
            comments
        };
    };
});