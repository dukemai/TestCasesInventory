define(['App/TestRun/Models/testsuitemodel', 'templateHelper', 'promise', 'underscore'], function (testSuiteModel, templateHelper, promise, _) {
    function testSuitePopUpView(id, testRunId) {
        this.model = new testSuiteModel(id);
        this.template = '';
        this.testRunId = testRunId;
    }

    testSuitePopUpView.prototype.render = function () {
        var self = this;
        var promisedResult = templateHelper.loadAndCache('test-suite-popup', '/ClientSide/Templates/TestRun/testsuites-in-popup.html');
        if (promisedResult) {
            promisedResult.then(function () {
                self.template = templateHelper.templates['test-suite-popup'];
                self.model.loadTestCases(self.testRunId).then(function () {
                    if (self.model.TestCases.length > 0) {
                        $('#test-suite-content-body-' + self.model.ID).append(self.template(self.model.TestCases));
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