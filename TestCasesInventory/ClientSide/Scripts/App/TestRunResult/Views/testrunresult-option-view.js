define(['App/TestRunResult/Models/testrunmodel', 'templateHelper', 'promise', 'underscore', 'App/TestRunResult/Views/testrunresult-pop-up-view'],
    function (testRunModel, templateHelper, promise, _, testCasesSlide) {
        function testRunResultOption(id) {
            this.model = new testRunModel(id);
            this.template = '';

            $(document).on('loading.view', function () {
                $('.loader').show();
            }).on('loadingCompleted.view', function () {
                setTimeout(function () {
                    $('.loader').hide();
                    $(".btn-group-set-status").show();
                }, 500);
            });
        }

        function registerEvents(testRunView) {
        }
        function unRegisterEvents() {
        }

        testRunResultOption.prototype.render = function () {
            var self = this;
            $(document).trigger('loading.view');
            self.model.getTestRunResult().then(function () {
                $(document).trigger('loadingCompleted.view');
                var testCaseView = new testCasesSlide(self.model.TestRunResult.ID);
                testCaseView.render();
                registerEvents(self);
            });
        }

        testRunResultOption.prototype.dispose = function () {
            unRegisterEvents();
            $(".btn-group-set-status").hide();
            $('#modalContent-run-testrun').empty();
        }
        return testRunResultOption;
    });