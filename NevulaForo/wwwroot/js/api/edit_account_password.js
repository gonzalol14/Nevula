const formEditGeneral = document.getElementById('formEditGeneral')

formEditGeneral.addEventListener('submit', (event) => {
    event.preventDefault()

    const pass = document.getElementById('password');
    const newpass = document.getElementById('newpassword');

    var validForm = 0;

    //CONTRASEÑA
    if (!pass.value.trim()) {
        failValidation(pass.parentElement, 'Debe ingresar la contraseña actual.', 'currentPass')

    } else {
        successValidation(pass.parentElement, 'currentPass')
        validForm++
    }

    //CONFIRMAR CONTRASEÑA
    if (!newpass.value.trim()) {
        failValidation(newpass.parentElement, 'Debe ingresar una nueva contraseña.', 'newPass')

    } else if (newpass.value.length < 3 || newpass.value.length > 50) {
        failValidation(newpass.parentElement, 'Debe ingresar una combinación de entre 3 y 50 caracteres.', 'newPass')

    } else {
        successValidation(newpass.parentElement, 'newPass')
        validForm++
    }

    if (validForm == 2) {
        const passValue = pass.value.trim() || null
        const newpassValue = newpass.value.trim() || null

        axios.post('/Account/EditPassword', { currentPass: passValue, newPass: newpassValue })
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    console.log('Datos actualizados correctamente')
                } else {
                    if (response.data.redirectUrl != null) {
                        window.location.href = response.data.redirectUrl
                    }

                    Object.keys(response.data.errors).forEach(fieldName => {
                        const errorMessage = response.data.errors[fieldName].join('\n');
                        const input = document.querySelector(`[name="${fieldName}"]`);
                        if (input) {
                            failValidation(input.parentElement, errorMessage, fieldName)
                        }
                    });
                }
            })
            .catch(error => {
                console.log("Error atrapado:", error);
            });
    }

})


const failValidation = (input, msj, passwordFather = null) => {
    const element = document.querySelector(`[data-valmsg-for="${input.getAttribute('name')}"]`) || document.querySelector(`[data-valmsg-for="${passwordFather}"]`);
    element.classList.remove("field-validation-valid");
    element.classList.add("field-validation-error");

    input.classList.add('input-validation-error')

    element.innerText = msj;
};
const successValidation = (input, passwordFather = null) => {
    const element = document.querySelector(`[data-valmsg-for="${input.getAttribute('name')}"]`) || document.querySelector(`[data-valmsg-for="${passwordFather}"]`);
    element.classList.remove("field-validation-error");
    element.classList.add("field-validation-valid");

    input.classList.remove('input-validation-error')

    element.innerText = null;
};