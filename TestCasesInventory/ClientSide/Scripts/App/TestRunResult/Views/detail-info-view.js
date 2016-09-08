define(['templateHelper','promise', 'underscore'],
    function (templateHelper, promise, _) {
        function detailInfoView(data) {
            this.model = data;
            this.template = '';
        }

        detailInfoView.prototype.render = function () {
            var self = this;
            var promisedResult = templateHelper.loadAndCache('detail-info', '/ClientSide/Templates/TestRunResult/testrun-result-detail-info.html');
            if (promisedResult) {
                promisedResult.then(function () {
                    self.template = templateHelper.templates['detail-info'];
                    $('#detail-info').append(self.template(self.model));
                })
            }
        }

        return detailInfoView;
    });