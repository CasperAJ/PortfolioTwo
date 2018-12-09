define(['knockout', 'dataService'], function (ko, ds) {
    return function (params) {
        var posts = ko.observableArray();
        var next = ko.observable("");
        var prev = ko.observable("");
        var getPostsApi = ko.observable("api/posts");

        var clickNextPrev = function (link) {
            getPostsApi(link);
            callPosts();
        };

        function callPosts() {
            ds.getPosts(getPostsApi(), function (data) {
                console.log(data.data, "DATADATAD");
                posts(data.data);

                nextLink = data.paging.next;
                prevLink = data.paging.prev;
                if (nextLink !== null) {
                    next(nextLink.replace("http://localhost:5000/", ""));
                    console.log(next());
                }
                if (prevLink !== null) {
                    console.log(prev());
                    prev(prevLink.replace("http://localhost:5000/", ""));
                }
            });    
        }

        callPosts();
        
        //postman.subscribe("deletePerson", function (id) {
        //    persons.remove(function (x) {
        //        return x.id === id;
        //    });
        //});

        return {
            posts,
            next,
            prev,
            clickNextPrev
        };
    };
});