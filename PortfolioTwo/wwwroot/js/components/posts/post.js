define(['knockout', 'dataService'], function (ko, ds) {
    return function (params) {
        
        var acceptedAnswer =params.post.acceptedAnswer;
        var author = params.post.author;
        var body = params.post.body;
        var closedDate = params.post.closedDate;
        var creationDate = params.post.creationDate;
        var linkPost = params.post.linkPost;
        var parent = params.post.parent;
        var path = params.post.path;
        var score = params.post.score;
        var title = params.post.title;

        var postDetailsPath = ko.observable("");
        var postDetailsPathConfiguretPath = ko.observable("");

         var clickPostDetails = function (p) {
             postDetailsPath = p.path;
             postDetailsPathConfiguretPath = postDetailsPath.replace("http://localhost:5000/", "");
             console.log(postDetailsPathConfiguretPath);
             console.log("Helloo");
             //ds.getPostDetails(postDetailsPathConfiguretPath(), function (data) {
             //        console.log("******");
             //        console.log(data);
             //        console.log("******");
             //    });
           
         };


      

        
        return {
            clickPostDetails,
            acceptedAnswer,
            author,
            body,
            closedDate,
            creationDate,
            linkPost,
            parent,
            path,
            score,
            title
        };
    };
});