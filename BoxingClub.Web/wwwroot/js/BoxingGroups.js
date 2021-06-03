(function ($) {
    $(document).ready(function () {
        var form = $("#boxingGroupForm");
        var boxingGroupId;
        var isCreateRequest;
        var dataType = 'application/x-www-form-urlencoded';
        var deletedData;

        $.fn.initValidation = function (elementSelector) {
            $(elementSelector).removeData("validator").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse(elementSelector);
        };

        $.fn.disableEdits = function () {
            $(".editButton").each(function () {
                $(this).addClass("disabled");
            });
        }

        $.fn.enableEdits = function () {
            $(".editButton").each(function () {
                $(this).removeClass("disabled");
            });
        }

        $.fn.disableNew = function () {
            $("#createButton").addClass("disabled");
        };

        $.fn.enableNew = function () {
            $("#createButton").removeClass("disabled");
        };

        form.submit(function (e) {
            var submitButton = $(this).find('input[type=submit], button[type=submit]');
            submitButton.prop('disabled', true);
            e.preventDefault();

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
                        submitButton.prop('disabled', false);
                        setTimeout(function () {
                            location.reload();
                        }, 10);
                    },
                    error: function () {
                        var isExists = $('#sendError').length;
                        console.log(isExists);
                        if (!isExists) {
                            var spanError = $(submitButton).parent().closest("tr").find('td:first').find('div');
                            spanError.append('<span id = "sendError" class = "text-danger">Error occurred while processing your request</span>');
                        }
                        console.log("Error occurred while processing your request");
                        submitButton.prop('disabled', false);
                    }
                })
            }
            else {
                submitButton.prop('disabled', false);
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
            $.get("Home/DeleteBoxingGroup/" + id, function () {
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
}(jQuery))