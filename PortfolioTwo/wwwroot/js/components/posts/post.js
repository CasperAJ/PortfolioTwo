define(["jquery", "knockout", 'dataService', 'postman', 'app'], function($, ko, ds, postman, vm) {
    return function(params){
        var singlePost = ko.observable();
        var marking = ko.observable(false);
        var noteVal = ko.observable();
        

        ds.getSinglePost(function(data) {
            singlePost(data);
        });

        ds.checkPostMark(function(data) {
            if(data != ""){
                marking(true);
                noteVal(data.note);
            }
        });

        var ChangeLayout = function(){
            postman.publish("changeComponent");
        };

        var BackToList = function(){
            postman.publish("changeComponent", "posts-list");
        }

        var MarkPost = function(){
            ds.createMark(function(){
                marking(true);
            }, noteVal());
        }

        var UnMarkPost = function(){
            ds.deleteMark(function(){
                marking(false);
            });
        }

        


        return {
            singlePost,
            ChangeLayout,
            BackToList,
            MarkPost,
            marking,
            UnMarkPost,
            noteVal
        };
    };
});