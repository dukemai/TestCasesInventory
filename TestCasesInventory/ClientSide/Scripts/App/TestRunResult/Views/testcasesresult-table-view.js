define(['templateHelper'], function (templateHelper) {
    function testCasesTable(TestCasesData) {
        this.model = TestCasesData;
        this.template = '';
    }

    testCasesTable.prototype.render = function () {
        var self = this;
        var promisedResult = templateHelper.loadAndCache('testcases-result-table', '/ClientSide/Templates/TestRunResult/TestCasesResult/testcases-result-table.html');
        if (promisedResult) {
            promisedResult.then(function () {
                self.template = templateHelper.templates['testcases-result-table'];
                $('#testcases-result-table').append(self.template(self.model));
            });
        }
    }

    return testCasesTable;
});