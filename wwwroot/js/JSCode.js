function scrollToBottom(container) {
    container.scrollTop = container.scrollHeight;
}

function isAtBottom(container) {
    //clientHeight is the amount of the div a user sees.
    return container.scrollTop + container.clientHeight >= container.scrollHeight;
}