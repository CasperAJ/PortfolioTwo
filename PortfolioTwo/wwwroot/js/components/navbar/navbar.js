define(["jquery", "knockout", 'dataService', 'postman', 'app'], function ($, ko, ds, postman, vm) {
    return function (params) {
       var inputSearch = ko.observable('h');
        var dropdownButton = ko.observable({ name: "Select Search", link: "api/Searches/bestrank" }); 
        console.log(params);
        var searchMode = ko.observableArray([
            { name: "Exact Match", link: "api/Searches/exact" },
            { name: "Best Match", link: "api/Searches/bestrank" },
            { name: "TFIDF Match", link: "api/Searches/besttfidf" }]);

      

        var clickSearch = function() {
            vm.currentListSearchValue(inputSearch());
            console.log(dropdownButton().link);
            postman.publish("postListStateChanged", dropdownButton().link);
            postman.publish("searchActivated");
            postman.publish("changeComponent", 'posts-list');
           
        };

        var changeDropdown = function(data) {

            dropdownButton(data);
        };


        return {
            inputSearch,
            clickSearch,
            searchMode,
            changeDropdown,
            dropdownButton,
        };
    };
});