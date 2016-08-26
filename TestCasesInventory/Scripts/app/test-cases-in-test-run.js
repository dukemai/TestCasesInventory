var templates = {};

function return_html(appendTo, tmpl, data) {

    if (templates[tmpl] === undefined) {
        jQuery.get("/Templates/handlebars_template_" + tmpl + ".htm", function (resp) {
            templates[tmpl] = Handlebars.compile(resp);
            return_html(appendTo, tmpl, data);
        });
        return;
    }
    var template = templates[tmpl];
    var html = template(data);
    $(appendTo).empty();
    $(appendTo).html(html);
}


$('.modal-link').on("click", function (e) {
    e.preventDefault();
    var thisHref = this.href;
    $.get(thisHref, function (data, status) {
        var testRunID = data[0].TestRunID;
        return_html("#modalContent", "list_test_suite_popup", data);
        $('#modal-container').modal('show');
        $("#modalContent").on("click","a" ,function () {
            var testSuiteID = $(this).attr("data-id");
            if ($("#" + testSuiteID + " .panel-body").html().trim() == "")
            {
                $.get("TestRun/GetTestCasesInTestSuitePopUp?testSuiteID=" + testSuiteID + "&testRunID=" + testRunID, function (data, status) {
                    if (data != null)
                        return_html("#" + testSuiteID + " .panel-body", "list_test_cases_popup", data);
                });
            }
        })
    });
})



