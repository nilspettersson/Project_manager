var url = "https://localhost:44373/ProjectManager/";
$(function () {

    

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
                user: $("#user").val(),
                project: $("#projectName").val()
            }, 
            success: function (result) {
                location.reload(false);
            }
        });

    });
    $("#sprint_remove").click(function () {

        $.ajax({
            url: url + "RemoveSprint",
            type: "POST",
            data: {
                sprint: $("#sprint_id").val(),
                project: $("#projectName").val()
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
                user: $("#user").val(),
                project: $("#projectName").val(),
                sprint: $("#sprint_id").val(),
                type: $("#task_type").val(),
                difficulty: $("#task_difficulty").val()
            },
            success: function (result) {
                location.reload(false);
            }
        });

    });

    $("#user_create").click(function () {

        $.ajax({
            url: url + "AddUser",
            type: "POST",
            data: {
                name: $("#user_name").val(),
                user_role: $("#user_role").val(),
                user: $("#user").val(),
                project: $("#projectName").val()
            },
            success: function (result) {
                var json = JSON.parse(result);
                console.log(json);
                if (json.success) {
                    location.reload(false);
                }
                else {
                    alert(json.message);
                }
            }
        });

    });



});

function setSprintId(btn) {
    $("#sprint_id").val(btn.value);
}
