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
    $.ajax({
        url: url + "UpdateTaskState",
        type: "POST",
        data: {
            task: ev.dataTransfer.getData("text"),
            state: state,
            project: $("#projectName").val(),
        },
        success: function (result) {
            
        }
    });
}

