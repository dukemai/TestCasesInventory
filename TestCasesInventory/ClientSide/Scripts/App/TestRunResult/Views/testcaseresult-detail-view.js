define(['templateHelper'], function (templateHelper) {
    function detailView(testCasesResultData)
    {
        this.model = testCasesResultData;
        this.template = '';
    }

    detailView.prototype.render = function () {
        var self = this;
        var promisedResult = templateHelper.loadAndCache('testcases-result-detail', '/ClientSide/Templates/TestRunResult/TestCasesResult/testcase-result-detail.html');
        if (promisedResult) {
            promisedResult.then(function () {
                self.template = templateHelper.templates['testcases-result-detail'];
                $('#testcase-result-detail-info').append(self.template(self.model));
            });
        }
    }

    detailView.prototype.dispose = function () {
        $('#testcase-result-detail-info').empty();
    }

    return detailView;
});