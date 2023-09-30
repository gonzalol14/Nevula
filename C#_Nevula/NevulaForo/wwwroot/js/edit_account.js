var typeEdit = document.title.split(" ")[1]
console.log(typeEdit)

if (typeEdit == "perfil") {
    $(`.btn_edit-general`).addClass("active");

} else if (typeEdit == "avatar") {
    $(`.btn_edit-avatar`).addClass("active");

    var avatar_input = document.getElementById("avatar_input")

    document.getElementById("change_img").addEventListener("click", () => {
        avatar_input.click()
    })

    avatar_input.addEventListener("change", () => {
        document.getElementById("form_avatar").submit()
    })

} else {
    $(`.btn_edit-pass`).addClass("active");
}