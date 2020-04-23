$(function () {
    console.log("hello");
    $("#save").click(function () {
        //console.log($("#name").val());
        //console.log($("#description").val());
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


//@using(Ajax.BeginForm("CreateProject", "ProjectManager", new AjaxOptions{ HttpMethod = "POST" }))