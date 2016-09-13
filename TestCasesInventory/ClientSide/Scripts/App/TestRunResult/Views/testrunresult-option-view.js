define(['App/TestRunResult/Models/testrunmodel', 'templateHelper', 'promise', 'underscore', 'App/TestRunResult/Views/testrunresult-pop-up-view', 'App/TestRunResult/Common/options-run-common'],
    function (testRunModel, templateHelper, promise, _, testCasesSlide, options) {
        function testRunResultOption(id) {
            this.model = new testRunModel(id);
            this.template = '';
            this.selected = '';

            $(document).on('loading.test', function () {
                $('.loader').show();
            }).on('loadingCompleted.test', function () {
                setTimeout(function () {
                    $('.loader').hide();
                    $(".btn-group-set-status").show();
                }, 500);
            });
        }

        function registerEvents(optionView) {
            $('#btn-start-test').click(function (e) {
                e.preventDefault();
                var radioGroup = $("#select-run-options");
                optionView.selected = $("input[name=radio-box]:checked", radioGroup).val();

                $('#modalContent-run-testrun').empty();
                $(document).trigger('loading.test');

                var testCaseView = new testCasesSlide(optionView.model.TestRunResult.ID, optionView.selected);
                testCaseView.render();
                $(document).trigger('loadingCompleted.test');
            });
        }
        function unRegisterEvents() {
        }

        testRunResultOption.prototype.render = function () {
            var self = this;
            var promisedResult = templateHelper.loadAndCache('options-run-popup', '/ClientSide/Templates/TestRunResult/options-run-popup.html');
            if (promisedResult) {
                promisedResult.then(function () {
                    self.template = templateHelper.templates['options-run-popup'];
                    self.model.getTestRunResult().then(function () {
                        $('#modalContent-run-testrun').append(self.template);
                        registerEvents(self);
                    });
                })
            }
        }

        testRunResultOption.prototype.dispose = function () {
            unRegisterEvents();
            $(".btn-group-set-status").hide();
            $('#modalContent-run-testrun').empty();
        }
        return testRunResultOption;
    });