define([], function () {

    var exportModule = function () {
    };
    exportModule.init = function (el) {
        $('[data-toggle="tab"]').click(function (e) {
            e.preventDefault();
            var self = $(this);
            self.tab('show');
            $.cookie('activeTab', self.attr('data-tab-index'));
        });
    };

    return exportModule;
});