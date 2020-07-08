var url = "https://localhost:44373/ProjectManager/";
$(function () {
    console.log("init");

    init();


});


function init() {

    $("#allProjects").click(function () {
        $.ajax({
            url: url + "Users",
            type: "GET",
            data: {
                filter: "all projects",
                user: $("#user").val(),
            },
            success: function (result) {
                var doc = stringToHTML(result);
                var projects = doc.body.childNodes[3].childNodes[7].childNodes[11];
                $("#projects").html(projects.innerHTML);

                $("#filter").html("all projects");
            }
        });

    });

    $("#yourProjects").click(function () {
        $.ajax({
            url: url + "Users",
            type: "GET",
            data: {
                filter: "your projects",
                user: $("#user").val(),
            },
            success: function (result) {
                var doc = stringToHTML(result);
                var projects = doc.body.childNodes[3].childNodes[7].childNodes[11];
                $("#projects").html(projects.innerHTML);

                $("#filter").html("your projects");
            }
        });

    });

    $("#search").click(function () {
        $.ajax({
            url: url + "Users",
            type: "GET",
            data: {
                search: $("#searchText").val(),
                filter: $("#filter").html(),
                user: $("#user").val(),
            },
            success: function (result) {
                var doc = stringToHTML(result);
                var projects = doc.body.childNodes[3].childNodes[7].childNodes[11];
                $("#projects").html(projects.innerHTML);
            }
        });

    });

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

    $("#user_remove").click(function () {

        $.ajax({
            url: url + "RemoveUser",
            type: "POST",
            data: {
                user_id: $("#user_id").val(),
                project: $("#projectName").val()
            },
            success: function (result) {
                location.reload(false);
            }
        });

    });


}


function setSprintId(btn) {
    $("#sprint_id").val(btn.value);
}

function setUserId(btn) {
    $("#user_id").val(btn.value);
}


var stringToHTML = function (str) {
    var doc = new DOMParser().parseFromString(str, 'text/html');
    return doc;
};
