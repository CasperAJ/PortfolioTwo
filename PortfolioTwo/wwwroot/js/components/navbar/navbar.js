define(["jquery", "knockout", 'dataService', 'postman', 'app'], function ($, ko, ds, postman, vm) {
    return function (params) {
        var singlePost = ko.observable();


        ds.getSinglePost(function (data) {
            singlePost(data);
        });

        var changeDropdown = function () {
            postman.publish("changedropdownSearch");
        };


        return {
            singlePost,
            changeDropdown
        };
    };
});