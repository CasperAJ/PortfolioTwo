define(["jquery", "knockout", 'dataService', 'postman', 'app'], function ($, ko, ds, postman, vm) {
    return function (params) {
       var inputSearch = ko.observable('h');
        var dropdownButton = ko.observable({ name: "Select Search", link: "api/Searches/bestrank" }); 
        console.log(params);
        var searchMode = ko.observableArray([
            { name: "Exact Match", link: "api/Searches/exact" },
            { name: "Best Match", link: "api/Searches/bestrank" },
            { name: "Ranked Match", link: "api/Searches/bestrank" }]);

        var jsondata = ko.observable({
            searchstring: inputSearch() 
        });
            

        //var changeDropdown = function () {
        //    postman.publish("changedropdownSearch");
        //};
        console.log("HAAAAJJJJJJJJJJJJJJJJJJJJJJJJ");

        var clickSearch = function() {
            
            console.log(inputSearch());
            console.log(dropdownButton());
            jsondata().searchstring = inputSearch();
            console.log("jsondata", jsondata());


            ds.getPosts(dropdownButton().link, jsondata(), function (data) {
                console.log(data);
                
                //posts(data.data);
                postman.publish("postListStateChanged", dropdownButton().link);
                //next(data.paging.next);
                //prev(data.paging.prev);
            });
        };

        var changeDropdown = function(data) {
            console.log("ChangeDropdown");
            console.log(data);
            console.log(data.name);
            dropdownButton(data);
        };


        return {
            inputSearch,
            clickSearch,
            searchMode,
            changeDropdown,
            dropdownButton,
            jsondata
        };
    };
});