$('[data-toggle="tab"]').click(function (e) {
    e.preventDefault();
    var self = $(this);
    self.tab('show');
    $.cookie('activeTab', self.attr('data-tab-index'));
}).on("shown.bs.tab", function (e) {
    var id = $(e.target).attr("href");
    localStorage.setItem('selectedTab', id)
});