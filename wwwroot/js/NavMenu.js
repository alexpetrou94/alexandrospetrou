//TODO: figure out how to get the scroll event to work with C#

window.addEventListener("scroll", () => {
    if (this.scrollY > 0) {
        document.querySelector("#navbar").classList.add("sticky");
    } else {
        document.querySelector("#navbar").classList.remove("sticky");
    }
});