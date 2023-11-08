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

        axios.post('/Account/EditPasswordApi', { currentPass: passValue, newPass: newpassValue })
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    alertMsj('Contraseña actualizada correctamente', 'success')
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
                alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                console.log("Error atrapado:", error);
            });
    }

})
