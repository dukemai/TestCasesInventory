define(['App/TestRunResult/Models/testrunresultmodel', 'templateHelper', 'promise', 'underscore',
    'App/TestRunResult/testrunresult-routes'],
    function (testRunResultModel, templateHelper, promise, _, routes) {
        function testRunResultView(id) {
            this.model = new testRunResultModel(id);
            this.template = '';
        }

        function registerEvents(testRunResultView) {
            var self = testRunResultView;
            $('#submit-run-testrun').on('click.submit', function () {
                console.log("ok");
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
            $('#submit-run-testrun').off('click.submit');
        }

        testRunResultView.prototype.dispose = function () {
            unRegisterEvents();
            $('#modalContent-run-testrun').empty();
        }

        return testRunResultView;
    });