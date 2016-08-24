
$('.modal-link').click(function (e) {
    e.preventDefault();
    $('#modalContent').load(this.href, function () {
        $('#modal-container').modal('show');
    });
})

