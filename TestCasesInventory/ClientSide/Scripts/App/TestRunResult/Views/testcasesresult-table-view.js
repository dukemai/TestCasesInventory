define(['templateHelper', 'App/TestRunResult/Views/testcaseresult-detail-view'], function (templateHelper, DetailPopUp) {
    function testCasesTable(TestCasesData) {
        this.model = TestCasesData;
        this.template = '';
    }

    function registerEvents(testCasesResult) {
        $(".show-detail-popup").click(function (e) {
            e.preventDefault();
            self = $(this);
            testCasesResultID = self.attr('test-case-id');
            var testCaseResultData = _.find(testCasesResult.model, function (testcase) {
                return testcase.ID == testCasesResultID;
            })
            var detailView = new DetailPopUp(testCaseResultData);
            $("#test-cases-result-modal").modal('show').on('hide.bs.modal', function () {
                detailView.dispose();
            });
            detailView.render();
        });
    }

    testCasesTable.prototype.render = function () {
        var self = this;
        var promisedResult = templateHelper.loadAndCache('testcases-result-table', '/ClientSide/Templates/TestRunResult/TestCasesResult/testcases-result-table.html');
        if (promisedResult) {
            promisedResult.then(function () {
                self.template = templateHelper.templates['testcases-result-table'];
                $('#testcases-result-table').append(self.template(self.model));
                registerEvents(self);
            });
        }
    }

    return testCasesTable;
});