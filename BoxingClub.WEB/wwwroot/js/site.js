
$(document).ready(function () {
    var form = $("#boxingGroupForm");
    var boxingGroupId;
    var isCreateRequest;
    var dataType = 'application/x-www-form-urlencoded';
    var deletedData;

    (function ($) {
        $.fn.initValidation = function (elementSelector) {
            $(elementSelector).removeData("validator").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse(elementSelector);
        };
    }(jQuery));


    (function ($) {
        $.fn.disableEdits = function () {
            $(".editButton").each(function () {
                $(this).addClass("disabled");
            });
        }
    }(jQuery));

    (function ($) {
        $.fn.enableEdits = function () {
            $(".editButton").each(function () {
                $(this).removeClass("disabled");
            });
        }
    }(jQuery));

    (function ($) {
        $.fn.disableNew = function () {
            $("#createButton").addClass("disabled");
        }
    }(jQuery));

    (function ($) {
        $.fn.enableNew = function () {
            $("#createButton").removeClass("disabled");
        }
    }(jQuery));

    form.submit(function (e) {
        e.preventDefault();

        var data = $('#boxingGroupForm').serialize();
        var url;
        if (isCreateRequest) {
            url = "Home/CreateBoxingGroup";
        }
        else {
            url = "Home/EditBoxingGroup/" + boxingGroupId;
        }

        if (form.valid()) {
            var data = $('#boxingGroupForm').serialize();
            $.ajax({
                url: url,
                type: "POST",
                contentType: dataType,
                data: data,
                success: function () {
                    setTimeout(function () {
                        location.reload();
                    }, 10);
                }
            })
        };
    });


    $(document).on("click", "#createSubmit", function () {

        isCreateRequest = true;
    });

    $(document).on("click", "#editSubmit", function () {
        isCreateRequest = false;
    });


    $(document).on("click", ".editButton", function () {
        $.fn.disableEdits();
        $.fn.disableNew();

        var parentTr = $(this).parent().closest("tr");
        boxingGroupId = $(parentTr).attr("id");
        deletedData = "<tr id=" + boxingGroupId + ">" + parentTr.html() + "</tr>";

        $.get("Home/EditBoxingGroupInline/" + boxingGroupId, function (data) {
            $(parentTr).replaceWith(data);
        }).then(function () {
            $.fn.initValidation(form);
        });

    });


    $(document).on("click", ".deleteButton", function (e) {
        e.preventDefault();
        var parentTr = $(this).parent().closest("tr");
        var id = $(parentTr).attr("id");
        $.post("Home/DeleteBoxingGroup/" + id, function () {
            setTimeout(function () {
                location.reload();
            }, 10);
        })
    });


    $("#createButton").click(function (e) {
        e.preventDefault();
        $.get("Home/CreateBoxingGroupInline", function (data) {
            $("tbody").append(data);
        }).then(function () {
            $.fn.initValidation(form);
        });
        $.fn.disableEdits();
        $.fn.disableNew();
    });

    $(document).on("click", "#cancelCreate", function () {

        console.log($(this).parent().closest("tr"));
        $(this).parent().closest("tr").remove();

        $.fn.enableNew();
        $.fn.enableEdits();

    });

    $(document).on("click", "#cancelEdit", function () {

        var parentTr = $(this).parent().closest("tr");
        $(parentTr).replaceWith(deletedData);

        $.fn.enableEdits();
        $.fn.enableNew();
    });

})
