var typeEdit = document.title.split(" ")[1]

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

} else if (typeEdit == "cuenta"){
    $(`.btn_edit-disable`).addClass("active");
} else {
    $(`.btn_edit-pass`).addClass("active");
}


// Obtener referencias a los elementos DOM
const aside = document.querySelector('.aside__menu-edit');
const submenu = document.querySelector('.submenu__edit-profile');
const leftArrow = document.getElementById('left-arrow');
const rightArrow = document.getElementById('right-arrow');


if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
    // El usuario está en un dispositivo móvil
    leftArrow.style.display = 'none';
    rightArrow.style.display = 'none';

    submenu.classList.add('no-scrollbar');
} else {
    // El usuario está en una computadora

    if (window.innerWidth < 530) {
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