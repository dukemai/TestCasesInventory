
define(['handlebars', 'promise'], function (handlebars, promise) {

    var exportModule = {};
    exportModule.init = function (el) {


    };

    exportModule.templates = {};

    exportModule.displayTemplate = function (el, tmpl, data) {
        if (exportModule.templates[tmpl] === undefined) {
            return;
        }

        var template = exportModule.templates[tmpl];
        var html = template(data);
        el.html(html);
    }

    exportModule.loadAndCache = function (name, url) {
        if (exportModule.templates[name] === undefined) {
            return promise.resolve($.get(url)).then(function (data) {
                exportModule.templates[name] = handlebars.compile(data);;
            });
        } else {
            return new promise(function (resolve) {
                resolve();
            });
        }
    }

    return exportModule;
});