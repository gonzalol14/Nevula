
var avatar_input = document.getElementById("avatar_input")

document.getElementById("change_img").addEventListener("click", () => {
    avatar_input.click()
})

avatar_input.addEventListener("change", () => {
    document.getElementById("form_avatar").submit()
})