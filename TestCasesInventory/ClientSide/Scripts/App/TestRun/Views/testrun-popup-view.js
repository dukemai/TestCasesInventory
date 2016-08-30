define(['App/TestRun/Models/testrunmodel', 'templateHelper', 'promise', 'underscore', 'App/TestRun/Views/testsuite-popup-view',
    'App/TestRun/testrun-routes'],
    function (testRunModel, templateHelper, promise, _, testSuitePopUpView, routes) {
        function testRunView(id) {
            this.model = new testRunModel(id);
            this.template = '';
            this.testSuiteViews = [];

            $(document).on('loading.view', function () {
                $('.loader').show();
            }).on('loadingCompleted.view', function () {
                setTimeout(function () {
                    $('.loader').hide();
                }, 500);
            });
        }

        function addTestCasesToTestRun(testRunView, listToAdd) {
            if (listToAdd.length > 0) {
                return promise.resolve($.post(routes.addTestCasesToTestRun, {
                    testRunID: testRunView.model.ID,
                    testCases: listToAdd
                }))
            }
            else {
                return promise.resolve();
            }
        }

        function removeTestCasesFromTestRun(testRunView, listToRemove) {
            if (listToRemove.length > 0) {
                return promise.resolve($.post(routes.removeTestCasesFromTestRun, {
                    testRunID: testRunView.model.ID,
                    testCases: listToRemove
                }))
            }
            else {
                return promise.resolve();
            }
        }

        function registerEvents(testRunView) {
            var self = testRunView;
            $('#submit-button').on('click.submit', function () {
                var listTestCasesToAdd = _.map(self.model.getTestCasesToAdd(), function (testCase) {
                    return testCase.ID;
                });
                var listTestCasesToRemove = _.map(self.model.getTestCasesToRemove(), function (testCase) {
                    return testCase.ID;
                });
                promise
                    .all([addTestCasesToTestRun(self, listTestCasesToAdd), removeTestCasesFromTestRun(self, listTestCasesToRemove)])
                    .then(function () {
                        window.location.href = window.location.href;
                    });
            });

            $('.test-suite-container').on('show.bs.collapse', function () {
                var id = Number(this.getAttribute('data-id'));
                var testSuite = _.findWhere(self.model.TestSuites, { ID: id });
                var testSuitePopUp = _.findWhere(self.testSuiteViews, { id: id });

                if (!testSuitePopUp) {
                    testSuitePopUp = new testSuitePopUpView(testSuite, self.model.ID);
                    self.testSuiteViews.push(testSuitePopUp);
                    testSuitePopUp.render();
                }
            });
        }
        function unRegisterEvents() {
            $('#submit-button').off('click.submit');
            $('.test-suite-container').off('show.bs.collapse');
        }

        testRunView.prototype.render = function () {
            var self = this;
            var promisedResult = templateHelper.loadAndCache('test-run-popup', '/ClientSide/Templates/TestRun/testrun-popup.html');
            if (promisedResult) {
                promisedResult.then(function () {
                    self.template = templateHelper.templates['test-run-popup'];
                    $(document).trigger('loading.view');

                    self.model.loadTestSuites().then(function () {
                        $(document).trigger('loadingCompleted.view');
                        $('#modalContent').append(self.template(self.model.TestSuites));

                        registerEvents(self);
                    });

                })
            }
        }

        testRunView.prototype.dispose = function () {
            unRegisterEvents();
            $('#modalContent').empty();
        }
        return testRunView;
    });