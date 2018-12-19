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
        //TODO: change the searches to not need the user id in the url

        $.ajax({
            beforeSend: function(request) {
                request.setRequestHeader("bearer", vm.token());
            },
            url:"api/searches/"+vm.userid(),
            type:"GET",
            contentType:"application/json",
            dataType:"json",
            success: function(data){
              callback(data);
            }
          });


    };

    var getSinglePost = function(callback){
        $.ajax({
            beforeSend: function(request) {
                request.setRequestHeader("bearer", vm.token());
            },
            url:vm.currentListting(),
            type:"GET",
            contentType:"application/json",
            dataType:"json",
            success: function(data){
              callback(data);
            }
          });
    }

    var GetAnswers = function(callback){
        var postid = vm.currentPost().split("/").slice(-1)[0];
        var url = 'api/posts/' + postid + '/children';

        $.ajax({
            beforeSend: function(request) {
                request.setRequestHeader("bearer", vm.token());
            },
            url:url,
            type:"GET",
            contentType:"application/json",
            dataType:"json",
            success: function(data){
              callback(data);
            }
          });
    };


    var GetComments = function(callback, url){


        $.ajax({
            beforeSend: function(request) {
                request.setRequestHeader("bearer", vm.token());
            },
            url:url,
            type:"GET",
            contentType:"application/json",
            dataType:"json",
            success: function(data){
              callback(data);
            }
          });



    };

    var GetMarks = function(callback) {

        $.ajax({
            beforeSend: function(request) {
                request.setRequestHeader("bearer", vm.token());
            },
            url:"api/markings/"+vm.userid()+"/user",
            type:"GET",
            contentType:"application/json",
            dataType:"json",
            success: function(data){
              callback(data);
            }
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


    var createMark = function(callback, note){

        var postid = vm.currentPost().split("/").slice(-1)[0];

        var jsondata = {
            postid: postid,
            note: note
        };

        $.ajax({
            url:'/api/markings',
            type:"POST",
            data:JSON.stringify(jsondata),
            contentType:"application/json",
            dataType:"json",
            success: function(data){
              callback(data);
            }
          });
    }

    var deleteMark = function(callback){

        var postid = vm.currentPost().split("/").slice(-1)[0];

        var jsondata = {
            postid: postid
        };

        $.ajax({
            url:'/api/markings',
            type:"DELETE",
            data:JSON.stringify(jsondata),
            contentType:"application/json",
            dataType:"json",
            success: function(data){
              callback(data);
            }
          });
    }


    var checkPostMark = function(callback){
        var postid = vm.currentPost().split("/").slice(-1)[0];

        $.ajax({
            beforeSend: function(request) {
                request.setRequestHeader("bearer", vm.token());
            },
            url:"api/markings/"+postid,
            type:"GET",
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
        createUser,
        createMark,
        deleteMark,
        checkPostMark
    };
});