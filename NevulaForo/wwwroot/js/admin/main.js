var typeEdit = document.title.split(" ")[1]

if (typeEdit == "usuarios") {
    $(`.btn_admin-users`).addClass("active");

} else if (typeEdit == "publicaciones") {
    $(`.btn_admin-publications`).addClass("active");

} else {
    $(`.btn_admin-comments`).addClass("active");
}



// Obtener referencias a los elementos DOM
const aside = document.querySelector('.aside__menu-admin');
const submenu = document.querySelector('.submenu__admin');
const leftArrow = document.getElementById('left-arrow');
const rightArrow = document.getElementById('right-arrow');


if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
    // El usuario está en un dispositivo móvil
    leftArrow.style.display = 'none';
    rightArrow.style.display = 'none';

    submenu.classList.add('no-scrollbar');
} else {
    // El usuario está en una computadora
    if (window.innerWidth < 404) {
        // Manejar eventos de clic para los botones de flecha
        leftArrow.addEventListener('click', () => {
            submenu.style.justifyContent = 'start';
            leftArrow.style.display = 'none';

        });

        rightArrow.addEventListener('click', () => {
            submenu.style.justifyContent = 'end';
            rightArrow.style.display = 'none';


        });

        aside.addEventListener('mouseenter', () => {
            if (window.getComputedStyle(submenu).getPropertyValue('justify-content') == 'start') {
                rightArrow.style.display = 'inline-block';
            } else {
                leftArrow.style.display = 'inline-block';
            }

        });

        aside.addEventListener('mouseleave', () => {
            if (window.getComputedStyle(submenu).getPropertyValue('justify-content') == 'start') {
                rightArrow.style.display = 'none';
            } else {
                leftArrow.style.display = 'none';
            }
        });
    }
}