$('.container__verify').hover(function () {
    id = $(this).attr('id').split("-")[1];
    $(`#popup-verify-${id}`).addClass("show");
}, function () {
    $(`.popup-verify`).removeClass("show");
})