define(['knockout'], function (ko) {
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
        
        return {
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