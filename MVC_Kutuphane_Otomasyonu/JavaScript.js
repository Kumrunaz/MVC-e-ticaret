document.addEventListener("DOMContentLoaded", function () {

    var dropdown = document.getElementsByClassName("dropdown-btn");
    var i;

    console.log("JavaScript.js yüklendi, dropdown eleman sayısı:", dropdown.length);

    for (i = 0; i < dropdown.length; i++) {
        dropdown[i].addEventListener("click", function () {
            this.classList.toggle("active");
            var dropdownContent = this.nextElementSibling;

            if (!dropdownContent) {
                console.log("nextElementSibling bulunamadı");
                return;
            }

            if (dropdownContent.style.display === "block") {
                dropdownContent.style.display = "none";
            } else {
                dropdownContent.style.display = "block";
            }
        });
    }

});














