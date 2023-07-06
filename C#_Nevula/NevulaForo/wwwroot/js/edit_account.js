var typeEdit = document.title.split(" ")[0]
console.log(typeEdit)

if (typeEdit == "General") {
    $(`.btn_edit-general`).addClass("active");
} else if (typeEdit == "Avatar") {
    $(`.btn_edit-avatar`).addClass("active");
} else if (typeEdit == "Password") {
    $(`.btn_edit-pass`).addClass("active");
}