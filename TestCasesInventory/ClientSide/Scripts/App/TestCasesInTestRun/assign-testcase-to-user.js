define(['handlebars', 'templateHelper',
    'App/TestCasesInTestRun/Views/testcasesintestrun-pop-up-view'],
    function (handleBars, templateHelper, testCasesInTestRunPopUpView) {
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
            $('.modal-link-assignto').click(function (e) {
                e.preventDefault();
                var self = $(this);
                var id = self.attr('data-test-case-in-test-run');
                var view = new testCasesInTestRunPopUpView(id);

                $('#modal-container-assign-to-user').modal('show').on('hide.bs.modal', function () {
                    view.dispose();
                });

                view.render();
            });
        }

        function setupTheApp() {

        }

        return exportModule;
    });