define(['App/TestRun/Models/testrunmodel', 'templateHelper', 'promise', 'underscore', 'App/TestRun/Views/testsuite-popup-view'],
    function (testRunModel, templateHelper, promise, _, testSuitePopUpView) {
        function testRunView(id) {
            this.model = new testRunModel(id);
            this.template = '';
            this.testSuiteViews = [];

            $(document).on('loading.view', function () {
                $('.loader').show();
            }).on('loadingCompleted.view', function () {
                $('.loader').hide();
            });
        }

        function registerEvents(testRunView) {
            var self = testRunView;
            $('#submit-button').on('click.submit', function () {
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