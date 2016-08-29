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
        });
    }
})

$("#submit-button").on("click", function () {
    var objArray = [];
    var testRunID = $("#modalContent a").attr("data-test-run-id");
    $("input[name='test-case']").each(function (index, elem) {
        var ID = Number($(this).attr('TestCaseID'));
        var Checked = this.checked;
        var TestRunID = $(this).attr('TestRunID') == "" ? null : Number($(this).attr('TestRunID'));
        var TestSuiteID = Number($(this).attr('TestSuiteID'));
        var TestSuiteTitle = $(this).attr('TestSuiteTitle');
        var Title = $(this).attr('Title');
        var obj = {
            ID: ID,
            Checked: Checked,
            TestRunID: TestRunID,
            TestSuiteID: TestSuiteID,
            TestSuiteTitle: TestSuiteTitle,
            Title: Title
        };
        objArray.push(obj);
    });

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: "TestRun/AddTestCasesToTestRun?testRunID=" + testRunID,
        data: JSON.stringify({ testCases: objArray })
    });
})

