define(['App/TestCasesInTestRun/Models/testcasesintestrunmodel', 'templateHelper', 'promise', 'underscore',
    'App/TestCasesInTestRun/testcasesintestrun-routes'],
    function (testCasesInTestRunModel, templateHelper, promise, _, routes) {
        function testCasesInTestRunView(id) {
            this.model = new testCasesInTestRunModel(id);
            this.assignedTo = '';
            this.template = '';
        }

        function assignTestCaseToUser(testCaseInTestRunID, userID) {
            return promise.resolve($.post(routes.assignTestCaseToUser, {
                testCaseInTestRunID: testCaseInTestRunID,
                userID: userID
            }))
        }

        function registerEvents(testCasesInTestRunView) {
            var self = testCasesInTestRunView;
            $('#submit-assign-to').on('click.submit', function () {
                var userSelected = $("#assign-to-user").find(":selected");
                var userID = userSelected.attr('data-id');
                promise.resolve(assignTestCaseToUser(self.model.ID, userID))
                    .then(function () {
                        sessionStorage.setItem('showMessage', 'show');
                        location.reload();
                    });
            });
        }

        testCasesInTestRunView.prototype.render = function () {
            var self = this;
            var promisedResult = templateHelper.loadAndCache('testcasesintestrun-popup', '/ClientSide/Templates/TestCasesInTestRun/testcasesintestrun-popup.html');
            if (promisedResult) {
                promisedResult.then(function () {
                    self.template = templateHelper.templates['testcasesintestrun-popup'];
                    self.model.loadUsers().then(function () {
                        $('#modalContent-assign-to-user').append(self.template(self.model.Users));

                        $('#assign-to-user').val(self.assignedTo);
                        registerEvents(self);
                    });

                })
            }
        }

        function unRegisterEvents() {
            //$('#submit-button').off('click.submit');
            //$('.test-suite-container').off('show.bs.collapse');
        }

        testCasesInTestRunView.prototype.dispose = function () {
            unRegisterEvents();
            $('#modalContent-assign-to-user').empty();
        }

        return testCasesInTestRunView;
    });