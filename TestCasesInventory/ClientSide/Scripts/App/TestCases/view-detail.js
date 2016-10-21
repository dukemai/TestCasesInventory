define(['App/TestCases/Views/testcases-detail-view'],
    function (testCasesDetailView) {
        var exportModule = {

        };
        exportModule.init = function (el) {
            testCasesDetailView.attachTo(".testcases-detail");
            setupTheApp();
        };

        function setupTheApp() {

        }
        return exportModule;
    });