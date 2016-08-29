define(['App/TestRun/Models/testrunmodel', 'templateHelper', 'promise', 'underscore', 'App/TestRun/Views/testsuite-popup-view'],
    function (testRunModel, templateHelper, promise, _, testSuitePopUpView) {
        function testRunView(id) {
            this.model = new testRunModel(id);
            this.template = '';
            this.testSuiteViews = [];
        }

        testRunView.prototype.render = function () {
            var self = this;
            var promisedResult = templateHelper.loadAndCache('test-run-popup', '/ClientSide/Templates/TestRun/testrun-popup.html');
            if (promisedResult) {
                promisedResult.then(function () {
                    self.template = templateHelper.templates['test-run-popup'];
                    self.model.loadTestSuites().then(function () {
                        $('#modalContent').append(self.template(self.model.TestSuites));

                        $('#btn-save-popup').click(function () {
                            console.log(self.model);
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

                    });
                  
                })
            }
        }

        testRunView.prototype.dispose = function () {
            $('#modalContent').empty();
        }
        return testRunView;
    });