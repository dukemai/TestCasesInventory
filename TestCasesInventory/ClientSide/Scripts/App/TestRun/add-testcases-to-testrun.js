﻿define(['handlebars', 'templateHelper',
    'App/TestRun/Views/testrun-popup-view'],
    function (handleBars, templateHelper, testRunPopUpView) {
        var exportModule = {

        };
        exportModule.init = function (el) {
            bindEvents();
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
                var id = self.attr('data-test-run');
                var view = new testRunPopUpView(id);
                view.render();
                e.preventDefault();
            });
        }

        function loadTestSuites(testRunId) {
            var view = new testRunPopUpView(testRunId);
            view.render();
        }

        function loadTestCases(url) {
            $.get(url, function (data, status) {
                console.log(data);
            })
        }

        function setupTheApp() {

        }

        return exportModule;
    });