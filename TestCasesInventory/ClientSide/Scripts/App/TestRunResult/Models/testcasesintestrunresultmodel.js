define([], function () {
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
        this.RunBy = RunBy;
        this.LastRunDate = RunBy == null ?  "":new Date(parseInt(LastRunDate.substr(6)));
    }
    return testCasesInTestRunResultModel;
});