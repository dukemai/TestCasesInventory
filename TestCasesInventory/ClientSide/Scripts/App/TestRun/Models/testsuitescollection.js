define(['backbone', 'App/TestRun/Models/testsuitemodel'], function (Backbone, testSuite) {
    var testSuites = Backbone.Collection.extend({
        model: testSuite,
        parse: function (data) {
            var arr = _.map(data, function (testSuiteData) {
                return new testSuite({
                    ID: testSuiteData.ID,
                    Title: testSuiteData.Title
                });
            });
           
            return arr;
        }
    });

    return testSuites;
});