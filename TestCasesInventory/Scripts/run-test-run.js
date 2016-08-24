$(function () {
    $('body').on('click', '.modal-link', function (e) {
        e.preventDefault();
        $('#modal-body').load(this.href, function () {
            $('#modal-container').modal('show');
        });
    });
});
$('#modal-container').modal({
    backdrop: 'static',
    keyboard: false
})