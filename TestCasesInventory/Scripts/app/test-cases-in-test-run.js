

$('.modal-link').click(function (e) {
    e.preventDefault();
    var thisHref = this.href;
    $.get(thisHref, function (data, status) {
        var testRunID = data[0].TestRunID;
        var result = template(data);
        $("#modalContent").empty();
        $("#modalContent").append(result);
        $('#modal-container').modal('show');
        $("#modalContent a").click(function (e) {
            var testSuiteID = $(this).attr("data-id");
            $.get("TestRun/GetTestCasesInTestSuitePopUp?testSuiteID=" + testSuiteID + "&testRunID=" + testRunID, function (data, status) {
                console.log(data);
            })
        })
    });
})

