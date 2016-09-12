define(['handlebars'], function (handlebars) {

    var exportModule = {};
    exportModule.init = function (el) {
        handlebars.registerHelper("increase", function (value) {
            return parseInt(value) + 1;
        });
    };
    return exportModule;
});
