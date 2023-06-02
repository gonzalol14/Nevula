// Menu desplegable
const btn_list = document.querySelector('.list_menu')
const list_menu = document.querySelector('.container__list-buttons')

btn_list.addEventListener('click', () => {
    list_menu.classList.toggle('activo')

    $('#icon-list').toggleClass("bi-list bi-x-lg");
})


// Auto-resize de textareas
$("textarea").each(function () {
    this.setAttribute("style", "height:" + (this.scrollHeight) + "px;");
}).on("input", function () {
    this.style.height = 0;
    this.style.height = (this.scrollHeight) + "px";
});


// Dropdowns header
$( ".btn_dropdown" ).hover(
    function() {
        id = this.id.split("-")[1]
        $(".dropdown-" + id).stop(true).delay(300).slideDown(250);
    }, function() {
        $(".dropdown-" + id).stop(true).delay(0).slideUp(250);
    }
);