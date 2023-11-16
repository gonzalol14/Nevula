document.getElementById("btn_back-top").addEventListener("click", function () {
    const scrollStep = -window.scrollY / (500 / 15); // Ajusta la velocidad aquí
    const scrollInterval = setInterval(function () {
        if (window.scrollY !== 0) {
            window.scrollBy(0, scrollStep);
        } else {
            clearInterval(scrollInterval);
        }
    }, 15);
});