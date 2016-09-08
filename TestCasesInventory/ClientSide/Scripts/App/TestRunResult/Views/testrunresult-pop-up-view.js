define(['App/TestRunResult/Models/testrunresultmodel', 'templateHelper', 'promise', 'underscore',
    'App/TestRunResult/testrunresult-routes'],
    function (testRunResultModel, templateHelper, promise, _, routes) {
        function testRunResultView(id) {
            this.model = new testRunResultModel(id);
            this.template = '';
        }

        function runTestCase(testCasesInTestRunID, testRunResultID, status, comment) {
            var testCaseResult = {
                TestCasesInTestRunID: testCasesInTestRunID,
                Status: status,
                TestRunResultID: testRunResultID,
                Comment: comment
            };
            return promise.resolve($.post(routes.createTestCaseResult, { testCaseResult: testCaseResult }));
        }

        function finishTestRunResult(testRunResultID) {
            return promise.resolve($.post(routes.finishTestRunResult, { testRunResultID }));
        }

        function registerEvents(testRunResultView) {
            var self = testRunResultView;
            $('#myCarousel').carousel({ interval: false });
            setTestCaseResult($('#submit-pass'));
            setTestCaseResult($('#submit-fail'));
            setTestCaseResult($('#submit-skip'));
            doneTestRunResult($('#submit-done'));
        }

        

        function setTestCaseResult(resultSubmit) {
            resultSubmit.on('click.submit', function () {
                var myCarousel = $('#myCarousel');
                var testCasesInTestRunID = $('.item.active').attr('data-id');
                var testRunResult = 1;
                var comment = $('#comment-' + testCasesInTestRunID);
                var newStatus = resultSubmit.attr('data-status');
                var currentStatus = $('#currentStatus-' + testCasesInTestRunID);

                if (currentStatus.html() == "Skipped" && currentStatus.html() == newStatus) {
                    console.log("done");
                    currentStatus.html(newStatus);
                    myCarousel.carousel("next");
                }
                else {
                    promise.resolve(runTestCase(testCasesInTestRunID, testRunResult, newStatus, comment.val()))
                    .then(function () {
                        currentStatus.html(newStatus);
                        comment.html(comment.val());
                    })
                    .then(function () {
                        myCarousel.carousel("next");
                    });
                }
                
            });
        }

        function doneTestRunResult(doneSubmit) {
            doneSubmit.on('click.submit.done', function () {
                var testRunResultID = 1;
                promise.resolve(finishTestRunResult(testRunResultID))
                    .then(function () {
                        location.reload();
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
            $('#submit-done').off('click.submit.done');
        }

        testRunResultView.prototype.dispose = function () {
            unRegisterEvents();
            $('#modalContent-run-testrun').empty();
        }

        return testRunResultView;
    });