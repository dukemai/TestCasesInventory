define(['moment'], function (moment) {
    function testCasesInTestRunResultModel(id, title, priority, decription, precondition, expect, status, comment, priorityStyleClass, statusStyleClass, RunBy, LastRunDate) {
        this.ID = id;
        this.TestCaseTitle = title;
        this.TestCasePriority = priority;
        this.TestCaseDescription = decription;
        this.TestCasePrecondition = precondition;
        this.TestCaseExpect = expect;
        this.Status = status == null ? "Skipped" : status;
        this.Comment = comment;
        this.PriorityStyleClass = priorityStyleClass;
        this.StatusStyleClass = statusStyleClass;
        this.RunBy = status == "Skipped" ? null : RunBy;
        this.LastRunDate = this.RunBy == null ? "" : moment(new Date(parseInt(LastRunDate.substr(6)))).format('MM/DD/YYYY, h:mm:ss a');
    }
    return testCasesInTestRunResultModel;
});