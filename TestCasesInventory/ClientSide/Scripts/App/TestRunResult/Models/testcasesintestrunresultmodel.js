define([], function () {
    function testCasesInTestRunResultModel(id, title, priority, decription, precondition, expect, priorityStyleClass) {
        this.ID = id;
        this.TestCaseTitle = title;
        this.TestCasePriority = priority;
        this.TestCaseDescription = decription;
        this.TestCasePrecondition = precondition;
        this.TestCaseExpect = expect;
        this.PriorityStyleClass = priorityStyleClass;
    }
    return testCasesInTestRunResultModel;
});