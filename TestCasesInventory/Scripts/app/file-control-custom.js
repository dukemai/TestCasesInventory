
    $(document).ready(function () {
        $(".delete-item").click(function () { // Click to only happen on deleteItem links
            $("#item").val($(this).data('id'));
        });
    });
