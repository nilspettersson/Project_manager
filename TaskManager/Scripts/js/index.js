$(function () {

    $("#project_create").click(function () {
        $.ajax({
            url: "https://localhost:44373/ProjectManager/CreateProject",
            type: "POST",
            data: {
                name: $("#name").val(),
                description: $("#description").val()
            },
            success: function (result) {
                location.reload(false);
            }
        });

    });

    $("#sprint_create").click(function () {

        $.ajax({
            url: "https://localhost:44373/ProjectManager/CreateSprint",
            type: "POST",
            data: {
                name: $("#name").val()
            },
            success: function (result) {
                location.reload(false);
            }
        });

    });



});
