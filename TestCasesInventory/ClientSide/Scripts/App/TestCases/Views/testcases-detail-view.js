define(['templateHelper', 'App/TestCases/Models/testcasemodel', 'flight'], function (TemplateHelper, testCaseModel, flight) {
    return flight.component(testCasesDetailView);
    function testCasesDetailView() {

        this.attributes({
            popUpId: '#test-cases-modal',
            testCaseId : ""
        });


        this.dispose = function () {
        };

        this.render = function (e) {
            console.log(this.$node.attr('test-case-id'));
            e.preventDefault();
            this.showPopUp();
        };

        this.showPopUp = function () {
            var PopUp = this.getPopUp();
            PopUp.modal('show');
        };
        
        this.getPopUp = function () {
            return $(this.attr.popUpId);
        };

        this.after('initialize', function () {
            this.on('click', this.render);
            this.on(this.getPopUp, 'hide.bs.modal', this.dispose);
        });
    }
});