define(["jquery", "knockout", 'dataService', 'postman', 'app'], function ($, ko, ds, postman, vm) {
    return function (params) {
       var inputSearch = ko.observable('h');
        var dropdownButton = ko.observable({ name: "Select Search", link: "api/Searches/bestrank" }); 

        var searchMode = ko.observableArray([
            { name: "Exact Match", link: "api/Searches/exact" },
            { name: "Best Match", link: "api/Searches/bestrank" },
            { name: "TFIDF Match", link: "api/Searches/besttfidf" }]);

      

        var clickSearch = function() {
            vm.currentListSearchValue(inputSearch());
            postman.publish("postListStateChanged", dropdownButton().link);
            postman.publish("searchActivated");
            postman.publish("changeComponent", 'posts-list');
           
        };

        var changeDropdown = function(data) {

            dropdownButton(data);
        };

        var changeComponent = function(){
            postman.publish('changeComponent', 'markings');
        }


        return {
            inputSearch,
            clickSearch,
            searchMode,
            changeDropdown,
            dropdownButton,
            changeComponent
        };
    };
});