/// <reference path="services/dataservice.js" />
require.config({
    baseUrl: 'js',
    paths: {
        jquery: 'lib/jQuery/dist/jquery',
        knockout: 'lib/knockout/dist/knockout',
        text: 'lib/text/text',
        dataService : 'services/dataService',
        postman: 'services/postman'
    }
});

// register components
require(['knockout'], function (ko) {
    ko.components.register("posts-list",
        {
            viewModel: { require: 'components/postsList/postsList' },
            template: { require: 'text!components/postsList/postsListView.html' }
        });

    ko.components.register("post",
        {
            viewModel: { require: 'components/posts/post' },
            template: { require: 'text!components/posts/postView.html' }
        });

});


// vm and observers
require(['knockout', 'app', 'postman'], function (ko, vm, postman) {

    // registrations of subscriptions

    postman.subscribe("changeComponent", function(){
        console.log("hit");
        if (vm.currentComponent() === 'posts-list') {
            vm.currentComponent("post");
        }
        else {
            vm.currentComponent('post-list');
        };
    });

    postman.subscribe("currentPostChanged", function(link) {
        vm.currentPost(link);
    });

    postman.subscribe("postListStateChanged", function(link) {
        vm.currentListting(link);
    });



    ko.applyBindings(vm);
});
