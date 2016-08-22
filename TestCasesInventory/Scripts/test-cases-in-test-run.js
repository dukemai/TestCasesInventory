$(function () {
    // when a link is clicked with these attributes, bootstrap will display the href content in a modal dialog.
    $('body').on('click', '.modal-link', function (e) {
        e.preventDefault();
        $('#modalContent').load(this.href, function () {
            $('#modal-container').modal('show');
        });
    });
});
