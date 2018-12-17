define(["jquery", "knockout", 'dataService', 'postman', 'app'], function($, ko, ds, postman, vm) {
    return function(params){
        var searches = ko.observableArray();
        
        ds.getSearches(function(data) {
            searches(data.data);
        });

        var goToSearch = function(data){
            console.log(data);
            vm.currentListSearchValue(data.searchstring);
            postman.publish("changeComponent", 'posts-list');
        }


        return {
            searches,
            goToSearch
        };
    };
});