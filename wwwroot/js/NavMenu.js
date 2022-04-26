window.addEventListener("scroll", () => {
    if (this.scrollY > 0) {
        document.querySelector("#navbar").classList.add("sticky");
    } else {
        document.querySelector("#navbar").classList.remove("sticky");
    }
});