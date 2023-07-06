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