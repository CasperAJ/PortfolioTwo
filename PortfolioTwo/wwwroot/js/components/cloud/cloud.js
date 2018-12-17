// note that we do not need a reference to jqcloud
// as it is a extention to jquery
define(['jquery', 'knockout', 'dataService', 'postman', 'jqcloud'], function ($, ko, ds, postman) {
    return function (params) {
        // just added some elements to the array so we can
        // get the binding to work
        var words = ko.observableArray([
            { text: "A", rank: 13 },
            { text: "B", rank: 10.5 }]);

        // to be able to use a asynchronous functon call
        // we need to implement updates on the cloud, since
        // the data will first be available after the creation
        // of the cloud
        ds.getWords(function(data) {
            words(data);
        });

        
        return {
            words
        };
    };
});