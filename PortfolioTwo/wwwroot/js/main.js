/// <reference path="services/dataservice.js" />
require.config({
    baseUrl: 'js',
    paths: {
        jquery: 'lib/jQuery/dist/jquery',
        knockout: 'lib/knockout/dist/knockout',
        text: 'lib/text/text',
        dataService : 'services/dataService',
        postman: 'services/postman',
        jqcloud: 'lib/jqcloud2/dist/jqcloud'
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

    ko.components.register("navbar",
        {
            viewModel: { require: 'components/navbar/navbar' },
            template: { require: 'text!components/navbar/navbarView.html' }
        });
    
    ko.components.register('answers',
        {
            viewModel: { require: 'components/answers/answers'},
            template: { require: 'text!components/answers/answersView.html'}
        });

    ko.components.register('comments',
        {
            viewModel: { require: 'components/comments/comments'},
            template: { require: 'text!components/comments/commentsView.html'}
        });

    ko.components.register('markings',
        {
            viewModel: { require: 'components/markings/markings'},
            template: { require: 'text!components/markings/markingsView.html'}
        });
    
    ko.components.register('searches',
        {
            viewModel: { require: 'components/searches/searches'},
            template: { require: 'text!components/searches/searchesView.html'}
        });
        
    ko.components.register('cloud',
        {
            viewModel: { require: 'components/cloud/cloud'},
            template: { require: 'text!components/cloud/cloudView.html'}
        });   

    ko.components.register('login',
        {
            viewModel: { require: 'components/login/login'},
            template: { require: 'text!components/login/loginView.html'}
        });  
});


// custum bindings
require(['jquery', 'knockout', 'jqcloud'], function($, ko) {
    ko.bindingHandlers.cloud = {
        init: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called when the binding is first applied to an element
            // Set up any initial state, event handlers, etc. here

            // we need to react to changes in the viewModel, so we need to subscribe to the words
            // and react if the array is changed.
            var cloud = allBindings.get('cloud');
            var words = cloud.words;

            // if we have words that is observables
            if (words && ko.isObservable(words)) {
                // then subscribe and update the cloud on changes
                words.subscribe(function() {
                    $(element).jQCloud('update', ko.unwrap(words));
                });
            }

        },
        update: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called once when the binding is first applied to an element,
            // and again whenever any observables/computeds that are accessed change
            // Update the DOM element based on the supplied values here.

            // we need to get the words from the bindings.
            // The allBindings parameter is an object with all the bindings
            // and we can get the 'cloud' binding 
            var cloud = allBindings.get('cloud');

            // from the cloud binding we want the array of words
            // we do not know wether this is an observable or not 
            // so the unwrap function is used to remove the observables if any
            // if words if not defined assign an empty array
            var words = ko.unwrap(cloud.words) || [];
            var width = cloud.height || 200;
            var height = cloud.height || 200;

            // to show the cloud we call the jqcloud function
            $(element).jQCloud(words,{
                width: width,
                height: height
            });
        }
    };
});


// vm and observers
require(['knockout', 'app', 'postman'], function (ko, vm, postman) {

    // registrations of subscriptions

    // postman.subscribe("changeComponent", function(){
    //     if (vm.currentComponent() === 'posts-list') {
    //         vm.currentComponent("post");
    //     }
    //     else {
    //         vm.currentComponent('post-list');
    //     };
    // });

    postman.subscribe("navbar", function () {
        vm.navbar();
    });



    postman.subscribe("changeComponent", function(newcomponent){
        vm.currentComponent(newcomponent);
    });

    postman.subscribe("currentPostChanged", function(link) {
        vm.currentPost(link);
    });

    postman.subscribe("postListStateChanged", function(link) {
        vm.currentListting(link);
        
    });

    postman.subscribe("tokenUpdated", function(token){
        vm.token(token);
        console.log(vm.token());
    });
    



    ko.applyBindings(vm);
});
