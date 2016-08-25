var listTestSuiteTemplate = '<div class="panel-group" id="pop-up-content" role="tablist" aria-multiselectable="false">' +
             ' {{#each .}}' +
              '<div class="panel panel-primary">' +
           '<div class="panel-heading" role="tab" id="{{Title}}">' +
                '<h4 class="panel-title">' +
                  '<a data-id="{{ID}}" role="button" data-toggle="collapse" data-parent="#pop-up-content" href="#{{ID}}" aria-expanded="false" aria-controls="{{ID}}">' +
                       '{{Title}}' +
                    '</a>'+
                 '</h4>' +
            '</div>'+
            '<div id="{{ID}}" class="panel-collapse collapse" role="tabpanel" aria-labelledby="{{Title}}">' +
                '<div class="panel-body">' +
                    '<div class=""></div>' +
                '</div>' +
            '</div>'+
        '</div>'+
             '{{/each}}';

var template = Handlebars.compile(listTestSuiteTemplate);
