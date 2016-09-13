define(['App/TestRunResult/Models/testrunresultmodel', 'templateHelper', 'promise', 'underscore', 'simplebar', 'handlebars',
    'App/TestRunResult/testrunresult-routes'],
    function (testRunResultModel, templateHelper, promise, _, simplebar, handlebars, routes) {
        function testRunResultView(id, selected) {
            this.model = new testRunResultModel(id);
            this.template = '';
            this.selected = selected;
        }

        function runTestCase(testCasesInTestRunID, testRunResultID, status, comment) {
            var testCaseResult = {
                TestCasesInTestRunID: testCasesInTestRunID,
                Status: status,
                testRunResultID: testRunResultID,
                Comment: comment
            };
            return promise.resolve($.post(routes.createTestCaseResult, { testCaseResult: testCaseResult }));
        }

        function finishTestRunResult(testRunResultID) {
            return promise.resolve($.post(routes.finishTestRunResult, { testRunResultID }));
        }

        function registerEvents(testRunResultView) {
            var self = testRunResultView;
            $('#myCarousel')
                .carousel({ interval: false })
                .on('slid.bs.carousel', function (e) {
                    var pagination = $('#run-test-pagination');
                    var index= $(this).find('.active').index();

                    $('.active.paging-li').removeClass('active');                    
                    $('[data-slide-to="'+index+'"]',pagination).addClass('active');
                });
            setTestCaseResult($('#submit-pass'), self);
            setTestCaseResult($('#submit-fail'), self);
            setTestCaseResult($('#submit-skip'), self);
            doneTestRunResult($('#submit-done'), self);
            $('#close-runtestrun').on('click.close', function () {
                location.reload();
            });
        }



        function setTestCaseResult(resultSubmit, testRunResultView) {
            var self = testRunResultView;
            var testRunResultID = self.model.ID;
            resultSubmit.on('click.submit', function () {
                var myCarousel = $('#myCarousel');
                var itemsNumber = $('.item').length;
                var activeItem = $('.item.active');
                var testCasesInTestRunID = activeItem.attr('data-id');
                var comment = $('#comment-' + testCasesInTestRunID);
                var newStatus = resultSubmit.attr('data-status');
                var currentStatus = $('#currentStatus-' + testCasesInTestRunID);

                if (currentStatus.html() != "" && currentStatus.html() != "Skipped" && newStatus == "Skipped") {
                    checkToFinish(activeItem, itemsNumber, self);
                    myCarousel.carousel("next");
                }
                else {
                    promise.resolve(runTestCase(testCasesInTestRunID, testRunResultID, newStatus, comment.val()))
                    .then(function () {
                        currentStatus.html(newStatus).prop('class', newStatus.toLowerCase());
                        comment.html(comment.val());
                        checkToFinish(activeItem, itemsNumber, self);
                        myCarousel.carousel("next");
                    });
                }

            });
        }

        function checkToFinish(activeItem, itemsNumber, testRunResultView) {
            var self = testRunResultView;
            if (activeItem.index() + 1 == itemsNumber) {
                showFinishDialog(self);
            }
        }

        function showFinishDialog(testRunResultView) {
            var self = testRunResultView;
            var testRunResultID = self.model.ID;
            var finishModal = $('#modal-container-finish-testrunresult');
            finishModal.modal({ backdrop: "static" });

            $('#cancel-finish').on('click.cancel.finish', function () {
                promise.resolve(finishModal.modal('hide'))
                    .then($('#cancel-finish').off('click.cancel.finish'));
            });
            $('#submit-finish').on('click.submit.finish', function () {
                promise.resolve(finishTestRunResult(testRunResultID))
                    .then(function () {
                        location.reload();
                    });
            })
        }

        function doneTestRunResult(doneSubmit, testRunResultView) {
            var self = testRunResultView;
            var testRunResultID = self.model.ID;
            doneSubmit.on('click.submit.done', function () {
                promise.resolve(finishTestRunResult(testRunResultID))
                    .then(function () {
                        showFinishDialog(self);
                    });

            });
        }

        testRunResultView.prototype.render = function () {
            var self = this;
            var promisedResult = templateHelper.loadAndCache('testrunresult-popup', '/ClientSide/Templates/TestRunResult/testrunresult-popup.html');
            if (promisedResult) {
                promisedResult.then(function () {
                    self.template = templateHelper.templates['testrunresult-popup'];
                    self.model.loadTestCasesInTestRunResults(self.selected).then(function () {
                        $('#modalContent-run-testrun').append(self.template(self.model.TestCasesInTestRunResults));
                        $('#myCarousel').simplebar();
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
            $('#close-runtestrun').off('click.close');
        }

        testRunResultView.prototype.dispose = function () {
            unRegisterEvents();
            $('#modalContent-run-testrun').empty();
        }

        return testRunResultView;
    });