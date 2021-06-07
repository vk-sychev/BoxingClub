(function ($) {
    $(document).ready(function () {
        $("#submitButton").click(function () {
            var data = $("#studentsForm").serialize();
            $("#studentsForm").submit();
        });

        $(".deleteButton").click(function () {
            var parentTr = $(this).parent().closest("tr");
            parentTr.remove();
        });
    });
}(jQuery));
