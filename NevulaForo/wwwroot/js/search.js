var searchFor = document.title.split(" ")[1]
console.log(searchFor)

if (searchFor == "publicaciones") {
    $(`.btn_search-posts`).addClass("active");

    document.getElementById("input_search-for").value = "posts"

} else if (searchFor == "cuentas") {
    $(`.btn_search-accounts`).addClass("active");

    document.getElementById("input_search-for").value = "accounts"

    var avatar_input = document.getElementById("avatar_input")
} 