// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Menu desplegable
const btn_list = document.querySelector('.list_menu')
const list_menu = document.querySelector('.container__list-buttons')

btn_list.addEventListener('click', () => {
    list_menu.classList.toggle('activo')

    $('#icon-list').toggleClass("bi-list bi-x-lg")
})


// Auto-resize de textareas
$("textarea").each(function () {
    this.setAttribute("style", "height:" + (this.scrollHeight + 2) + "px")
}).on("input", function () {
    this.style.height = 0
    this.style.height = (this.scrollHeight + 2) + "px"
})


// Dropdowns header
if ($("#container_dropdown-profile1").css("display") == "none") {
    //Dropdown por hover
    $(".container_dropdown").hover(
        function () {
            id = this.id.split("-")[1]
            $(".dropdown-" + id).stop(true).delay(300).slideDown(250)
        }, function () {
            $(".dropdown-" + id).stop(true).delay(0).slideUp(250)
        }
    )
} else {
    //Dropdown por click
    $(".container_dropdown").click(function (e) {
        var id = this.id.split("-")[1]
        $(this).children().first().attr("href", "#")

        //Todos los dropdowns
        dropdowns = $(".dropdown")
        for (i = 0; i < dropdowns.length; i++) {
            var btn_dropdown = $(dropdowns[i]).parent().children().first()

            if (dropdowns[i] != $(".dropdown-" + id)[0]) {
                // Cerrar los dropdowns abiertos, si es que hay
                $(dropdowns[i]).slideUp(250)

                if (btn_dropdown.hasClass("btn_dropdown-active")) {
                    btn_dropdown.removeClass("btn_dropdown-active")
                }
            } else {
                // Abrir el dropdown seleccionado
                $(".dropdown-" + id).slideToggle(250)
                console.log(id)
                if (id != "profile2") {
                    if (btn_dropdown.hasClass("btn_dropdown-active")) {
                        btn_dropdown.removeClass("btn_dropdown-active")
                        btn_dropdown.addClass("btn_dropdown-disabled")
                    } else {
                        btn_dropdown.removeClass("btn_dropdown-disabled")
                        btn_dropdown.addClass("btn_dropdown-active")
                    }

                }
            }
        }
    })
}