define(['knockout'], function (ko) {
    var title = "SOA Application";
    var currentComponent = ko.observable("posts-list");
    var currentPost = ko.observable();
    var currentListting = ko.observable("api/Searches/bestrank");
    var currentNavbar = ko.observable("navbar");
    var currentPostData = ko.observableArray(); 
    var currentListSearchValue = ko.observable("solutions");
    var currentPostList = ko.observable();
    var userid = ko.observable("2");
    var token = ko.observable();
    var userComponent = ko.observable("login");

    return {
        title,
        currentComponent,
        currentPost,
        currentListting,
        currentNavbar,
        currentPostData,
        currentListSearchValue,
        currentPostList,
        userid,
        token,
        userComponent
    };
});