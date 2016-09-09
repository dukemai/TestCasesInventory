define(['handlebars', 'templateHelper', 'App/TestRunResult/Views/testrunresult-option-view', 'promise',
        'App/TestRunResult/testrunresult-routes'],
    function (handleBars, templateHelper, testRunResultOptionView, promise, routes) {
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

            $('.modal-link-run-testrun').click(function (e) {
                e.preventDefault();
                var self = $(this);
                var testRunID = self.attr('data-test-run');
                var view = new testRunResultOptionView(testRunID);
                $('#modal-container-run-testrun').modal({ backdrop: "static" }).on('hide.bs.modal', function () {
                    view.dispose();
                });
                view.render();
            });

        }

        function setupTheApp() {

        }

        return exportModule;
    });