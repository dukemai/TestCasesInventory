define([], function () {
    function testCasesInTestRunResultModel(id, title, priority, priorityStyleClass, precondition, expect) {
        this.ID = id;
        this.Title = title;
        this.Priority = priority;
        this.Expect = expect;
        this.Precondition = precondition;
        this.PriorityStyleClass = priorityStyleClass;
    }
    return testCasesInTestRunResultModel;
});