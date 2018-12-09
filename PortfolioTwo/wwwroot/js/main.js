﻿/// <reference path="services/dataservice.js" />
require.config({
    baseUrl: 'js',
    paths: {
        jquery: 'lib/jQuery/dist/jquery',
        knockout: 'lib/knockout/dist/knockout',
        text: 'lib/text/text',
        dataService : 'services/dataService'
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

require(['knockout', 'app/viewModel'], function (ko, vm) {
    ko.applyBindings(vm);
});
