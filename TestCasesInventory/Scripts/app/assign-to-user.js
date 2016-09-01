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

$('.modal-link-assign-to-user').on("click", function (e) {
    e.preventDefault();
    var thisHref = this.href;
    $.get(thisHref, function (data, status) {
        return_html("#modal-content-assign-to-user", "list_users_assign_test_case", data);
        $('#modal-container-assign-to-user').modal('show');
    });
})

$("#submit-assign-to-user").on("click", function () {
    var objArray = [];
    var selected = $("#assign-user").find(":selected");
    var UserID = selected.attr('value');
    var Email = selected.attr('Email');
    var DisplayName = selected.text();
    var TestCaseInTestRunID = Number(selected.attr('TestCaseInTestRunID'));
    var TeamID = Number(selected.attr('TeamID'));
    var obj = {
        ID: UserID,
        TeamID: TeamID,
        TestCaseInTestRunID: TestCaseInTestRunID,
        DisplayName: DisplayName,
        Email: Email
    };
    objArray.push(obj);
    var myURL = "Admin/TestCasesInTestRun/AssignToUser";
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: "http://testcasesinventory.test:8021/Admin/TestCasesInTestRun/AssignToUser?testCaseInTestRunID=" + TestCaseInTestRunID,
        data: JSON.stringify({ user: objArray })
    });
    
    $('#modal-container-assign-to-user').modal('hide');
})