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
    $(appendTo).html(html);
}

$('.modal-link').on("click", function (e) {
    e.preventDefault();
    var thisHref = this.href;
    $.get(thisHref, function (data, status) {
        return_html("#modalContent", "list_test_suite_popup", data);
        $('#modal-container').modal('show');
    });
})


$("#modalContent").on("click", "a", function () {
    var testSuiteID = $(this).attr("data-test-suite-id");
    var testRunID = $(this).attr("data-test-run-id");
    if ($("#" + testSuiteID + " .panel-body").html().trim() == "") {
        $.get("TestRun/GetTestCasesInTestSuitePopUp?testSuiteID=" + testSuiteID + "&testRunID=" + testRunID, function (data, status) {
            return_html("#" + testSuiteID + " .panel-body", "list_test_cases_popup", data);
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                url: "TestRun/AddTestCasesToTestRun?testRunID=" + testRunID,
                data: JSON.stringify({ testCases: data })
            });
        });
    }
})

$("#submit-button").on("click", function () {
    console.log($("#modalContent").html());

    //duyet nhung thang <input> test case.
    //attr.checked, is in testrun = true.
    //submit action ve server.
})

