define(['knockout', 'dataService', 'app', 'postman'], function (ko, ds, vm, postman) {
    return function (params) {
        var posts = ko.observableArray();
        var next = ko.observable();
        var prev = ko.observable();
        var getPostsApi = ko.observable("api/Searches/bestrank");

        // computed
        var prevEnable = ko.computed(function(){
            return prev() != null;
        });

        var nextEnable = ko.computed(function(){
            return next() != null;
        });

        var clickNextPrev = function (link) {
            getPostsApi(link);
            callPosts();
        };

        var showPost = function (data) {
            var postlink = 'http://localhost:5000/api/posts/' + data.id;
            vm.currentPost(postlink);
            postman.publish("currentPostChanged", postlink);
            postman.publish("changeComponent");

        };

        function callPosts() {
            var jsondata = {
                searchstring: "solutions"
            };
            
            ds.getPosts(getPostsApi(), jsondata, function (data) {
                posts(data.data);
                postman.publish("postListStateChanged", getPostsApi());
                next(data.paging.next);
                prev(data.paging.prev);
            });    
        }

        callPosts();
        

        return {
            posts,
            next,
            prev,
            clickNextPrev,
            prevEnable,
            nextEnable,
            showPost
        };
    };
});