
    $(document).ready(function () {
        $(".deleteItem").click(function () { // Click to only happen on deleteItem links
            $("#item").val($(this).data('id'));
            //$('#createFormId').modal('show');
        });
    });
