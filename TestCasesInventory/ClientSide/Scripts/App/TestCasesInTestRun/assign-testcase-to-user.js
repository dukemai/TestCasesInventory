define(['handlebars', 'templateHelper', 'App/TestCasesInTestRun/Views/testcasesintestrun-pop-up-view', 'promise',
        'App/TestCasesInTestRun/testcasesintestrun-routes'],
    function (handleBars, templateHelper, testCasesInTestRunPopUpView, promise, routes) {
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
                var assignedTo = self.attr('data-assignedto');
                var view = new testCasesInTestRunPopUpView(id);
                view.assignedTo = assignedTo;
                $('#modal-container-assign-to-user').modal('show').on('hide.bs.modal', function () {
                    view.dispose();
                });

                view.render();
            });

            $('.modal-link-assign-to-me').click(function (e) {
                e.preventDefault();
                var self = $(this);
                var id = self.attr('data-id');
                var assignedTo = self.attr('data-assignedto');
                promise.resolve($.post(routes.assignTestCaseToMe, { id: id }))
                    .then(function () {
                        sessionStorage.setItem('showMessage', 'show');
                        location.reload();
                    });
            });


            $(document).ready(function () {
                if (sessionStorage.getItem('showMessage') == 'show') {
                    $('.show-message').show().delay(700).fadeOut(1000);
                    sessionStorage.removeItem('showMessage');
                }
            })
        }

        function setupTheApp() {

        }

        return exportModule;
    });