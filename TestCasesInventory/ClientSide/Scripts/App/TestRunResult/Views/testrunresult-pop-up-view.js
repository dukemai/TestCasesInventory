define(['App/TestRunResult/Models/testrunresultmodel', 'templateHelper', 'promise', 'underscore',
    'App/TestRunResult/testrunresult-routes'],
    function (testRunResultModel, templateHelper, promise, _, routes) {
        function testRunResultView(id) {
            this.model = new testRunResultModel(id);
            this.template = '';
        }

        function registerEvents(testRunResultView) {
            var self = testRunResultView;
            runTestCase($('#submit-pass'));
            runTestCase($('#submit-fail'));
            runTestCase($('#submit-skip'));
        }

        function runTestCase(resultSubmit) {
            resultSubmit.on('click.submit', function () {
                $('#myCarousel').carousel("next");
                console.log(resultSubmit.val());
            });
        }

        testRunResultView.prototype.render = function () {
            var self = this;
            var promisedResult = templateHelper.loadAndCache('testrunresult-popup', '/ClientSide/Templates/TestRunResult/testrunresult-popup.html');
            if (promisedResult) {
                promisedResult.then(function () {
                    self.template = templateHelper.templates['testrunresult-popup'];
                    self.model.loadTestCasesInTestRunResults().then(function () {
                        $('#modalContent-run-testrun').append(self.template(self.model.TestCasesInTestRunResults));

                        registerEvents(self);
                    });

                })
            }
        }

        function unRegisterEvents() {
            $('#submit-pass').off('click.submit');
            $('#submit-fail').off('click.submit');
            $('#submit-skip').off('click.submit');
        }

        testRunResultView.prototype.dispose = function () {
            unRegisterEvents();
            $('#modalContent-run-testrun').empty();
        }

        return testRunResultView;
    });