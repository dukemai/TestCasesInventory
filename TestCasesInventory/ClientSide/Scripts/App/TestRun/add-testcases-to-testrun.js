define(['handlebars', 'templateHelper', 'backbone', 'underscore'], function (handleBars, templateHelper, Backbone, _) {

    var exportModule = {};
    var routes = {};
    routes.getTestSuite = '/Admin/TestRun/GetTestSuitesPopUp/';
    routes.getTestCasesForTestSuite = '/Admin/TestRun/GetTestSuitesPopUp/';
    var templates = {
        'test-run-popup': '/ClientSide/Templates/TestRun/test-run-popup.html',
        'test-cases-in-popup': '/ClientSide/Templates/TestRun/test-cases-in-popup.html'
    }

    exportModule.init = function (el) {
        bindEvents();
        loadTemplates();
        setupTheApp();
    };

    function loadTemplates() {
        for (var i in templates) {
            templateHelper.loadAndCache(i, templates[i]);
        }
    }

    function bindEvents() {
        $('#show-testrun-popup').click(function (e) {
            var self = $(this);
            $('#modal-container').modal('show');
            loadTestSuites(self.attr('data-test-run'));
            e.preventDefault();
        });
    }

    function loadTestSuites(testRunId) {
        $.get(routes.getTestSuite + testRunId, function (data, status) {
            templateHelper.displayTemplate($('#modalContent'), 'test-run-popup', data);
        })
    }

    function loadTestCases(url) {
        $.get(url, function (data, status) {
            console.log(data);
        })
    }

    function setupTheApp() {
        var testSuites = Backbone.Collection.extend({
            defaults: {
                testRunId: 0
            },
            model: testSuite,
            urlRoot: routes.getTestSuite
        });

        var testSuite = Backbone.Model.extend({
            defaults: {
                id: '',
                title: ''
            },
            loadTestCases: function () {
                var self = this;
                $.get(routes.getTestCasesForTestSuite + self.get('id'), function (data) {
                    var testCases = self.testCases;
                    for (var i = 0; i < data.length; i++) {
                        self.testCases.push(new testCase(data[i]));
                    }
                });
            }
        });

        var testRun = Backbone.Model.extend({
            defaults: {
                id: '',
                testSuiteCollection: new testSuites()
            },
            loadTestSuites: function () {
                var self = this;
                self.testSuiteCollection = new testSuites({ testRunId: self.id });
                console.log(self);
                return;                
                self.testSuiteCollection.fetch({
                    success: function (collection, response) {
                        console.log(collection);
                    }
                })
            }
        });

       

       

        var testRunObject = new testRun({ id: 1 });
        var testSuiteCollection = new testSuites();
        console.log(testRunObject);
        console.log(testSuiteCollection);
        testRunObject.loadTestSuites();
        /*
        var testSuite = Backbone.Model.extend({
            defaults: {
                id: '',
                title: '',
                testCases: []
            },
            loadTestCases: function () {
                var self = this;
                $.get(routes.getTestCasesForTestSuite + self.get('id'), function (data) {
                    var testCases = self.testCases;
                    for (var i = 0; i < data.length; i++) {
                        self.testCases.push(new testCase(data[i]));
                    }
                });
            }
        });

        var testCase = Backbone.Model.extend({
            defaults: {
                id: '',
                title: ''
            },
            init: function (data) {
                return this;
            },
            loadTestCases: function () {

            }
        });

        var popUpView = Backbone.View.extend({

            template: templateHelper.templates['test-run-popup'],

            render: function () {

                this.$el.html(this.template());

                return this;

            }

        });

        return popUpView;
        */
    }

    function setupCollection(testRunId) {
        var testSuites = Backbone.Collection.extend({
            url: routes.getTestSuite + testRunId,
            parse: function (data) {
                return data;
            }
        });

        return testSuites;
    }

    return exportModule;
});