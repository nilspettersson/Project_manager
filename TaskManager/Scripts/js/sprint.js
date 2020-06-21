function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev, el) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    el.appendChild(document.getElementById(data));

    var state = el.id;
    var task = ev.id;

    $.ajax({
        url: url + "UpdateTaskState",
        type: "POST",
        data: {
            task: task,
            state: state,
            project: $("#projectName").val(),
        },
        success: function (result) {
            location.reload(false);
        }
    });
}

