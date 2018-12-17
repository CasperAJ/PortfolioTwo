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
    
    var getSearches = function(callback) {
        $.getJSON("api/searches/"+vm.userid(), function(data) {
            callback(data);
        });
    };

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

    var getWords = function(callback){

        var jsondata = {
            searchstring: vm.currentListSearchValue()
        };

        $.ajax({
            url:'/api/searches/cloud/simple',
            type:"POST",
            data:JSON.stringify(jsondata),
            contentType:"application/json",
            dataType:"json",
            success: function(data){
              callback(data);
            }
          });
    }

    var authenticate = function(callback, username, password){
        var jsondata = {
            username: username,
            password: password
        };

        $.ajax({
            url:'/api/users/login',
            type:"POST",
            data:JSON.stringify(jsondata),
            contentType:"application/json",
            dataType:"json",
            success: function(data){
              callback(data);
            }
          });
    }


    var createUser = function(callback, username, password, email){
        var jsondata = {
            email: email,
            username: username,
            password: password
        };

        $.ajax({
            url:'/api/users',
            type:"POST",
            data:JSON.stringify(jsondata),
            contentType:"application/json",
            dataType:"json",
            success: function(data){
              callback(data);
            }
          });
    }


    return {
        getPosts,
        getSinglePost,
        GetAnswers,
        GetComments,
        GetMarks,
        getSearches,
        getWords,
        authenticate,
        createUser
    };
});