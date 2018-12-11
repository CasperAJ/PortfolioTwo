define(['jquery', 'app'], function ($, vm) {

    // var getPosts = function (data, callback) {   
    //     $.getJSON(data, function(response) {
    //         callback(response);
    //     });
    // };

    var getPosts = function(url, data, callback){
        $.ajax({
            url:url,
            type:"POST",
            data:JSON.stringify(data),
            contentType:"application/json",
            dataType:"json",
            success: function(data){
                console.log(data);
              callback(data);
            }
          });
    }
    


    // var getPostDetails = function (data, callback) {
    //     $.getJSON(data, function (response) {
    //         callback(response);
    //     });
    // };

    var getSinglePost = function(callback){
        console.log("from dataservice");
        console.log(vm.currentPost());
        $.getJSON(vm.currentPost(), function(data) {
           callback(data);
        });
    }

    var GetAnswers = function(callback){
        var postid = vm.currentPost().split("/").slice(-1)[0];
        var url = 'api/posts/' + postid + '/children';
        $.getJSON(url, function(data) {
            callback(data);
        });
    };


    var GetComments = function(callback, url){
        $.getJSON(url, function(data) {
            callback(data);
        });
    };


    return {
        getPosts,
        getSinglePost,
        GetAnswers,
        GetComments
    };
});