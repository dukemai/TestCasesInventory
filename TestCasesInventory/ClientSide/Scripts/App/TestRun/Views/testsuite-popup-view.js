define(['App/TestRun/Models/testsuitemodel', 'templateHelper', 'promise', 'underscore'], function (testSuiteModel, templateHelper, promise, _) {
    function testSuitePopUpView(testSuite, testRunId) {
        this.model = testSuite;
        this.template = '';
        this.testRunId = testRunId;
        this.id = testSuite.ID;
    }

    testSuitePopUpView.prototype.render = function () {
        var self = this;
        if (!self.isRendered) {
            self.isRendered = true;
        }
        else {
            return;
        }
        var promisedResult = templateHelper.loadAndCache('test-suite-popup', '/ClientSide/Templates/TestRun/testsuites-in-popup.html');
        if (promisedResult) {
            promisedResult.then(function () {
                self.template = templateHelper.templates['test-suite-popup'];
                $(document).trigger('loading.view');
                self.model.loadTestCases(self.testRunId).then(function () {
                    $(document).trigger('loadingCompleted.view');
                    if (self.model.TestCases.length > 0) {
                        var testSuiteBody = $('#test-suite-content-body-' + self.model.ID);
                        testSuiteBody.append(self.template(self.model.TestCases));
                        $('.checkbox-testcase', testSuiteBody).change(function () {
                            var id = Number(this.getAttribute('data-id'));
                            var testCase = _.findWhere(self.model.TestCases, { ID: id });
                            testCase.Checked = $(this).is(':checked');
                        });
                    }
                });
            })
        }
    }

    testSuitePopUpView.prototype.dispose = function () {
        $('#test-suite-content-body' + this.id).empty();
    }
    return testSuitePopUpView;
});