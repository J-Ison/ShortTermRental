document.addEventListener("DOMContentLoaded", function () {
    var toggler = document.getElementById("navToggle");
    var nav = document.getElementById("navbarNav");

    if (!toggler || !nav) return;

    toggler.addEventListener("click", function () {
        if (nav.classList.contains("show")) {
            nav.classList.remove("show");
        } else {
            nav.classList.add("show");
        }
    });
});
