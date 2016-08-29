define([], function () {
    function testCaseModel(id, title, checked) {
        this.ID = id;
        this.Title = title;
        this.Checked = checked;
    }
    return testCaseModel;
});