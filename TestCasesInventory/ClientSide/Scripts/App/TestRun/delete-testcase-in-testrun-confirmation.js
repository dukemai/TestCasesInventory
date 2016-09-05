define([], function () {

    var exportModule = {};
    exportModule.init = function (el) {
        $(".delete-testcase").click(function () { // Click to only happen on deleteTestCase links
            $("#testCasesInTestRunID").val($(this).attr('data-testcase-id'));
        });
    };
    return exportModule;
});