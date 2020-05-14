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
}