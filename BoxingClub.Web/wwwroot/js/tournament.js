(function ($) {
    console.log("ready1");
    $(document).ready(function () {
        console.log("ready2");
        $("#submitButton").click(function () {
            var data = $("#studentsForm").serialize();
            console.log(data);
            $("#studentsForm").submit();
        });

        $(".deleteButton").click(function () {
            console.log("ready3");
            var parentTr = $(this).parent().closest("tr");
            parentTr.remove();
        });
    });
}(jQuery));
