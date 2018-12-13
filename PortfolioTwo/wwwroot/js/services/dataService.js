define(['jquery', 'app'], function ($, vm) {


    var getPosts = function (callback) {

        var jsondata = {
            searchstring: vm.currentListSearchValue()
        };
        $.ajax({
            url:vm.currentListting(),
            type:"POST",
            data:JSON.stringify(jsondata),
            contentType:"application/json",
            dataType:"json",
            success: function(data){
              callback(data);
            }
          });
    }
    

    var getSinglePost = function(callback){
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

    var GetMarks = function(callback) {
        $.getJSON("api/markings/"+vm.userid()+"/user", function(data){
            callback(data);
        });
    }


    return {
        getPosts,
        getSinglePost,
        GetAnswers,
        GetComments,
        GetMarks
    };
});