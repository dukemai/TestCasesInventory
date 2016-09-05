define([], function () {

    var exportModule = {};
    exportModule.init = function (el) {
        $(".delete-item").click(function () { // Click to only happen on deleteItem links
            $("#item").val($(this).data('id'));
        });
    };

    return exportModule;
});