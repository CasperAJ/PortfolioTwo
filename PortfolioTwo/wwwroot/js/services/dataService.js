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
        $.getJSON(vm.currentPost(), function(data) {
           callback(data);
        });
    }

    var GetAnswers = function(url, callback){
        $.getJSON(url, function(data) {
            callback(data);
        });
    };


    return {
        getPosts,
        getSinglePost,
        GetAnswers
    };
});