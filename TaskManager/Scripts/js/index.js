$(function () {

    $("#save").click(function () {
        $.ajax({
            url: "https://localhost:44373/ProjectManager/CreateProject",
            type: "POST",
            data: {
                name: $("#name").val(),
                description: $("#description").val()
            },
            success: function (result) {
                //console.log(result);
                location.reload(false);
            }
        });

    });



});
