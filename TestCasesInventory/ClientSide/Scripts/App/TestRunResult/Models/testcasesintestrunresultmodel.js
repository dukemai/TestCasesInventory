﻿define([], function () {
    function testCasesInTestRunResultModel(id, title, priority, decription, precondition, expect, status, comment, priorityStyleClass) {
        this.ID = id;
        this.TestCaseTitle = title;
        this.TestCasePriority = priority;
        this.TestCaseDescription = decription;
        this.TestCasePrecondition = precondition;
        this.TestCaseExpect = expect;
        this.Status = status;
        this.Comment = comment;
        this.PriorityStyleClass = priorityStyleClass;
    }
    return testCasesInTestRunResultModel;
});