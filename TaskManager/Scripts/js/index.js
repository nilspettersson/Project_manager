$(function () {

    var url = "https://localhost:44373/ProjectManager/"

    $("#project_create").click(function () {
        $.ajax({
            url: url + "CreateProject",
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
            url: url + "CreateSprint",
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

    $("#task_create").click(function () {

        $.ajax({
            url: url + "CreateTask",
            type: "POST",
            data: {
                name: $("#task_name").val(),
                description: $("#task_description").val(),
                user: $("#sprint_user").val(),
                project: $("#sprint_projectName").val(),
                sprint: $("#sprint_id").val()
            },
            success: function (result) {
                location.reload(false);
            }
        });

    });



});
