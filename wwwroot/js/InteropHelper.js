function sendAlert(msg) {
    alert(msg);
}

function addClassByID(id, pClass) {
    const element = document.getElementById(id);
    element.classList.add(pClass);
}

function addClassByClass(elementClass, pClass) {
    const elements = document.getElementsByClassName(elementClass);

    for(var i = 0; i < elements.length; i++)
    {
        elements[i].classList.add(pClass);
    }
}

function toggleClassByID(id, pClass) {
    const element = document.getElementById(id);
    element.classList.toggle(pClass);
}

function toggleClassByClass(elementClass, pClass) {
    const elements = document.getElementsByClassName(elementClass);

    for (var i = 0; i < elements.length; i++) {
        elements[i].classList.toggle(pClass);
    }
}

function removeClassByID(id, pClass) {
    const element = document.getElementById(id);
    element.classList.remove(pClass);
}

function removeClassByClass(elementClass, pClass) {
    const elements = document.getElementsByClassName(elementClass);

    for (var i = 0; i < elements.length; i++) {
        elements[i].classList.remove(pClass);
    }
}

function hasClassByID(id, pClass) {
    const element = document.getElementById(id);
    return element.classList.contains(pClass);
}