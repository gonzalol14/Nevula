const formEditAvatar = document.getElementById('formEditAvatar')

var avatar_input = document.getElementById("avatar")

document.getElementById("change_img").addEventListener("click", () => {
    avatar_input.click()
})

avatar_input.addEventListener("change", () => {
    const formData = new FormData();
    const input = document.getElementById('avatar');
    const avatar = input.files[0]

    var allowedExtensions = ['.jpg', '.png', '.webp', '.gif'];
    var avatarName = avatar.name.toLowerCase() || null;

    var validForm = 0;
    
    //AVATAR
    if (avatar.size > 3 * 1024 * 1024) {
        failValidation(input, 'El archivo debe ser menor o igual a 3 MB.')
    } else if (!allowedExtensions.some(ext => avatarName.endsWith(ext))) {
        failValidation(input, 'Se permiten archivos: .jpg, .png, .webp, .gif')
    } else {
        successValidation(input)
        validForm++
    }

    if (validForm == 1) {
        formData.append('Avatar', avatar);

        axios.post('/Account/EditAvatarApi', formData)
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    console.log('Datos actualizados correctamente')
                    const imagenes = document.querySelectorAll('.profile_pic-edit-avatar')

                    imagenes.forEach((imagen) => {
                        imagen.src = response.data.pathProfilePic
                    });

                    document.getElementById('header__profile-pic').src = response.data.pathProfilePic

                } else {
                    if (response.data.redirectUrl != null) {
                        window.location.href = response.data.redirectUrl
                    }

                    Object.keys(response.data.errors).forEach(fieldName => {
                        const errorMessage = response.data.errors[fieldName].join('\n');
                        const input = document.querySelector(`[name="${fieldName}"]`);
                        if (input) {
                            failValidation(input, errorMessage)
                        }
                    });
                }
            })
            .catch(error => {
                console.log("Error atrapado:", error);
            });
    }
})

const failValidation = (input, msj) => {
    const element = document.querySelector(`[data-valmsg-for="${input.getAttribute('name')}"]`)
    element.classList.remove("field-validation-valid");
    element.classList.add("field-validation-error");

    element.innerText = msj;
};
const successValidation = (input) => {
    const element = document.querySelector(`[data-valmsg-for="${input.getAttribute('name')}"]`)
    element.classList.remove("field-validation-error");
    element.classList.add("field-validation-valid");

    element.innerText = null;
};


const deleteAvatar = document.getElementById('edit_avatar-delete')

deleteAvatar.addEventListener('click', (event) => {
    axios.get('/Account/DeleteAvatarApi')
        .then(response => {
            // Manejar la respuesta exitosa aquí
            console.log(response.data)
            if (response.data.success) {
                console.log("Avatar eliminado correctamente")

                const imagenes = document.querySelectorAll('.profile_pic-edit-avatar')

                imagenes.forEach((imagen) => {
                    imagen.src = response.data.pathProfilePic
                });

                document.getElementById('header__profile-pic').src = response.data.pathProfilePic
            }
        })
        .catch(error => {
            console.log("Error atrapado:", error);
        });
})