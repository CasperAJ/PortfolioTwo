define(['knockout', 'dataService', 'app', 'postman'], function (ko, ds, vm, postman) {
    return function (params) {
        var posts = ko.observableArray();
        var next = ko.observable();
        var prev = ko.observable();
        var getPostsApi = ko.observable(vm.currentListting());
        var searchValue = ko.observable(vm.currentListSearchValue());

        // computed
        var prevEnable = ko.computed(function(){
            return prev() != null;
        });

        var nextEnable = ko.computed(function(){
            return next() != null;
        });

        var clickNextPrev = function (link) {
            console.log("clicked value: " + next());
            getPostsApi(link);
            callPosts();
        };

        var showPost = function (data) {
            var postlink = 'api/posts/' + data.id;
            postman.publish("currentPostChanged", postlink);
            postman.publish("changeComponent", 'post');
        };

        function callPosts() {
            
            ds.getPosts(function (data) {
                postman.publish("postListStateChanged", getPostsApi());
                posts(data.data);
                next(data.paging.next);
                console.log("new value: " + next());
                prev(data.paging.prev);
            });    
        }

       postman.subscribe("searchActivated", function(){
           console.log("search updated hit");
           callPosts();
       });

        callPosts();


        var goToCloud = function(){
            postman.publish("changeComponent", 'cloud');
        }


        return {
            posts,
            next,
            prev,
            clickNextPrev,
            prevEnable,
            nextEnable,
            showPost,
            goToCloud
        };
    };
});