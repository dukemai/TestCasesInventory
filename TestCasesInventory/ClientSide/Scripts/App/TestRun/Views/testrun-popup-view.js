define(['App/TestRun/Models/testrunmodel', 'templateHelper', 'promise'], function (testRunModel, templateHelper) {
    function testRunView(id) {
        this.model = new testRunModel(id);
        this.template = '';
    }

    testRunView.prototype.render = function () {
        var self = this;
        var promisedResult = templateHelper.loadAndCache('test-run-popup', '/ClientSide/Templates/TestRun/testrun-popup.html');
        if (promisedResult) {
            promisedResult.then(function () {
                self.template = templateHelper.templates['test-run-popup'];
                self.model.loadTestSuites().then(function () {
                    console.log(self.model.TestSuites);
                    $('#modalContent').empty().append(self.template(self.model.TestSuites));
                });

            })
        }
    }

    return testRunView;
});