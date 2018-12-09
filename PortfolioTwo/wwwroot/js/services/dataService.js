define(['jquery'], function ($) {

    var getPosts = function (data, callback) {   
        $.getJSON(data, function(response) {
            callback(response);
        });
    };

    var getPostDetails = function (data, callback) {
        $.getJSON(data, function (response) {
            callback(response);
        });
    };

    return {
        getPosts,
        getPostDetails
    };
});