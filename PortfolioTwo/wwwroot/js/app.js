define(['knockout'], function (ko) {
    var title = "SOA Application";
    var currentComponent = ko.observable("posts-list");
    var currentPost = ko.observable();
    var currentListting = ko.observable("api/posts");
    return {
        title,
        currentComponent,
        currentPost,
        currentListting
    };
});