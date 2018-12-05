require.config({
    baseUrl: 'js',
    paths: {
        jquery: 'lib/jQuery/dist/jquery',
        knockout: 'lib/knockout/dist/knockout'
    }
});


require(['knockout', 'app/viewModel'], function (ko, vm) {
    ko.applyBindings(vm);
});
