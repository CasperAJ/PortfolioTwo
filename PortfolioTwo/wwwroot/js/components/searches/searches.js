define(["jquery", "knockout", 'dataService', 'postman', 'app'], function($, ko, ds, postman, vm) {
    return function(params){
        var searches = ko.observableArray();

        var next = ko.observable();
        var prev = ko.observable();


        // computed
        var prevEnable = ko.computed(function(){
            return prev() != null;
        });

        var nextEnable = ko.computed(function(){
            return next() != null;
        });

        var pagingNextClick = function(){
            getSearches(next());
        }

        var pagingPrevClick = function(){
            getSearches(prev());
        }
        
        ds.getSearches(function(data) {
            searches(data.data);
            next(data.paging.next);
            prev(data.paging.prev);
        });

        var getSearches = function(url){
            ds.getSearchesPages(function(data){
                searches(data.data);
                next(data.paging.next);
                prev(data.paging.prev);
            }, url);
        }

        var goToSearch = function(data){
            console.log(data);
            vm.currentListSearchValue(data.searchstring);
            postman.publish("changeComponent", 'posts-list');
        }


        return {
            searches,
            goToSearch,
            next,
            prev,
            nextEnable,
            prevEnable,
            getSearches,
            pagingNextClick,
            pagingPrevClick
        };
    };
});