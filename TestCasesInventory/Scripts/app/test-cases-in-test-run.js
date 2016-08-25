

$('.modal-link').click(function (e) {
    e.preventDefault();
    var thisHref = this.href;
    $.get(thisHref, function (data, status) {
        var result = template(data);
        $("#modalContent").empty();
        $("#modalContent").append(result);
        $('#modal-container').modal('show');
    });
})
