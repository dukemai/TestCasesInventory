define([], function () {
    function testCaseModel(id, title, checked, priority, createdDate, createdDisplayOnly, priorityStyleClass) {
        this.ID = id;
        this.Title = title;
        this.Checked = checked;
        this.Priority = priority;
        this.CreatedDate = createdDate;
        this.CreatedDisplayOnly = createdDisplayOnly;
        this.PriorityStyleClass = priorityStyleClass;
    }
    return testCaseModel;
});