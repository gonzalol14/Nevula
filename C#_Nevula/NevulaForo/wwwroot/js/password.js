(function ($) {
    "use strict";

    $(".toggle-password").click(function () {
        $(this).children("span").children("i").toggleClass("bi-eye-fill bi-eye-slash-fill");
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });
})(jQuery);



const input_password = document.getElementById("password")

const input_new_password = document.getElementById("newpassword") || null

if (input_password.classList.contains("input-validation-error")) {
    input_password.parentElement.classList.add("input-validation-error")
}

if (input_new_password.classList.contains("input-validation-error")) {
    input_new_password.parentElement.classList.add("input-validation-error")
}