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
            $('.modal-link-assign-to-user').click(function (e) {
                e.preventDefault();
                var self = $(this);
                var id = self.attr('data-id');
                var view = new testCasesInTestRunPopUpView(id);
                $('#modal-container-assign-to-user').modal('show').on('hide.bs.modal', function () {
                    view.dispose();
                });

                view.render();
            });

            $('.modal-link-assign-to-me').click(function (e) {
                e.preventDefault();
                var self = $(this);
                var id = self.attr('data-id');
                $.post("/Admin/TestCasesInTestRun/AssignToMe/", { id: id });
                location.reload();
            });
        }

        function setupTheApp() {

        }

        return exportModule;
    });