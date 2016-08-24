$('[data-toggle="tab"]').click(function (e) {
    e.preventDefault();
    $(this).tab('show');
}).on("shown.bs.tab", function (e) {
    var id = $(e.target).attr("href");
    localStorage.setItem('selectedTab', id)
});

//var selectedTab = localStorage.getItem('selectedTab');
//if (selectedTab != null) {
//    $('a[data-toggle="tab"][href="' + selectedTab + '"]').tab('show');
//}
