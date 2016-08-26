$(function () {
    $('.modal-link').on('click', function (e) {
        e.preventDefault();
        $('#modal-body').load(this.href, function () {
            $('#modal-container').modal({
                backdrop: 'static',
                keyboard: false
            });
        });
    });
});
