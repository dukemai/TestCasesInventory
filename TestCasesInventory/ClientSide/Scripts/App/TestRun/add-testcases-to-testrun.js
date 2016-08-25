define(['handlebars', 'templateHelper'], function (handleBars, templateHelper) {

    var exportModule = {};
    var routes = {};
    routes.getTestSuite = '/Admin/TestRun/GetTestSuitesPopUp/';
    var templates = {
        'test-run-popup': '/ClientSide/Templates/TestRun/test-run-popup.html'
    }

    exportModule.init = function (el) {
        bindEvents();
        loadTemplates();
    };

    function loadTemplates() {
        templateHelper.loadAndCache('test-run-popup', templates['test-run-popup']);
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

    return exportModule;
});