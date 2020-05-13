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
                name: $("#sprint_name").val(),
                start_time: $("#sprint_start_time").val(),
                weeks: $("#sprint_weeks").val(),
                user: $("#sprint_user").val(),
                project: $("#sprint_projectName").val()
            }, 
            success: function (result) {
                location.reload(false);
            }
        });

    });



});
