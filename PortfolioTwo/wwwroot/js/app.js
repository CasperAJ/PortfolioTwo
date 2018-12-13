define(['knockout'], function (ko) {
    var title = "SOA Application";
    var currentComponent = ko.observable("posts-list");
    var currentPost = ko.observable();
    var currentListting = ko.observable("api/Searches/bestrank");
    var currentNavbar = ko.observable("navbar");
    var currentPostData = ko.observableArray(); 

    return {
        title,
        currentComponent,
        currentPost,
        currentListting,
        currentNavbar,
        currentPostData
    };
});