define(['App/TestRunResult/Models/testrunresultmodel', 'templateHelper', 'promise', 'underscore',
    'App/TestRunResult/testrunresult-routes'],
    function (testRunResultModel, templateHelper, promise, _, routes) {
        function testRunResultView(id) {
            this.model = new testRunResultModel(id);
            this.template = '';
        }

        function registerEvents(testRunResultView) {
            var self = testRunResultView;
            $('#myCarousel').carousel({ interval: false });
            setTestCaseResult($('#submit-pass'));
            setTestCaseResult($('#submit-fail'));
            setTestCaseResult($('#submit-skip'));
        }

        function runTestCase(testCasesInTestRunID, testRunResultID, status) {
            var testCaseResult = {
                TestCasesInTestRunID: testCasesInTestRunID,
                Status: status,
                TestRunResultID: testRunResultID
            };
            console.log(testCaseResult);
            return promise.resolve($.post(routes.createTestCaseResult, { testCaseResult: testCaseResult }));
        }

        function setTestCaseResult(resultSubmit) {
            resultSubmit.on('click.submit', function () {
                var myCarousel = $('#myCarousel');
                var testCasesInTestRunID = $('.item.active').attr('data-id');
                var testRunResult = 1;
          
                promise.resolve(runTestCase(testCasesInTestRunID, testRunResult, resultSubmit.val()))
                    .then(function () {
                        myCarousel.carousel("next");
                    });
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