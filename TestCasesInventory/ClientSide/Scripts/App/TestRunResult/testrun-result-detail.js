define(['App/TestRunResult/Views/testrun-result-detail-view'],
    function (testRunResultDetailView) {
        var exportModule = {

        };
        exportModule.init = function (el) {
            bindEvents();
            setupTheApp();
        };

        function bindEvents() {
            $(document).ready(function () {
                var testRunResultDetail = $("#test-run-result-detail");
                if(testRunResultDetail.length)
                {
                    var id = testRunResultDetail.attr('test-run-result-id');
                    var view = new testRunResultDetailView(id);
                    view.render();
                }
            });
        }

        function setupTheApp() {

        }

        return exportModule;
    });