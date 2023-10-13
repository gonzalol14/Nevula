const formEditGeneral = document.getElementById('formEditGeneral')

const submitButton = document.getElementById('edit_general-submit');
const originalFormValues = {};
formEditGeneral.querySelectorAll('input, textarea').forEach(input => {
    originalFormValues[input.name] = input.value;
});

formEditGeneral.addEventListener('input', function () {
    let formChanged = false;

    // Verifica si algún valor ha cambiado
    formEditGeneral.querySelectorAll('input, textarea').forEach(element => {
        if (originalFormValues[element.name] !== element.value) {
            formChanged = true;
        }
    });

    // Habilita/deshabilita el botón de envío según si el formulario ha cambiado
    submitButton.disabled = !formChanged;
});

formEditGeneral.addEventListener('submit', (event) => {
    event.preventDefault()

    const newName = document.getElementById('name');
    const newSurname = document.getElementById('surname');
    const newUsername = document.getElementById('username');
    const newEmail = document.getElementById('email');
    const newDescription = document.getElementById('description');

    var validForm = 0;
    
    //NOMBRE
    if (newName.value.trim() && (newName.value.length < 3 || newName.value.length > 30)) {
        failValidation(newName, 'El nombre debe tener entre 3 y 30 caracteres.')
    } else {
        successValidation(newName)
        validForm++
    }

    //APELLIDO
    if (newSurname.value.trim() && (newSurname.value.length < 3 || newSurname.value.length > 30)) {
        failValidation(newSurname, 'El apellido debe tener entre 3 y 30 caracteres.')
    } else {
        successValidation(newSurname)
        validForm++
    }

    //EMAIL
    if (!newEmail.value.trim()) {
        failValidation(newEmail, 'Debe ingresar un correo electrónico.')

    } else if (!emailValidation(newEmail.value)) {
        failValidation(newEmail, 'Debe ingresar un correo electrónico válido.')

    } else if (newEmail.value.length > 50) {
        failValidation(newEmail, 'Correo electronico demasiado largo.')

    } else {
        successValidation(newEmail)
        validForm++
    }

    //USERNAME
    if (!newUsername.value.trim()) {
        failValidation(newUsername, 'Debe ingresar un nombre de usuario.')

    } else if (newUsername.value.length < 3 || newUsername.value.length > 30) {
        failValidation(newUsername, 'El nombre de usuario debe tener entre 3 y 30 caracteres.')

    } else {
        successValidation(newUsername)
        validForm++
    }

    //DESCRIPCION
    if (newDescription.value.trim() && newDescription.value.length > 250) {
        failValidation(newDescription, 'La descripción debe tener hasta 250 caracteres.')

    } else {
        successValidation(newDescription)
        validForm++
    }

    //CAMBIOS
    if (originalFormValues["Name"] != newName.value || originalFormValues["Surname"] != newSurname.value || originalFormValues["Username"] != newUsername.value ||
        originalFormValues["Email"] != newEmail.value || originalFormValues["Description"] != newDescription.value) {
        validForm++
    }

    if (validForm == 6) {
        const newNameValue = newName.value.trim() || null
        const newSurnameValue = newSurname.value.trim() || null
        const newUsernameValue = newUsername.value.trim() || null
        const newEmailValue = newEmail.value.trim() || null
        const newDescriptionValue = newDescription.value.trim() || null

        axios.post('/Account/EditGeneralApi', { Name: newNameValue, Surname: newSurnameValue, Username: newUsernameValue, Email: newEmailValue, Description: newDescriptionValue })
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    alertMsj('Perfil actualizado correctamente')

                    submitButton.disabled = true
                    //Actualizo los originalFormValues porque el usuario cambio sus datos fue actualizado
                    formEditGeneral.querySelectorAll('input').forEach(input => {
                        originalFormValues[input.name] = input.value;
                    });

                } else {
                    if (response.data.redirectUrl != null) {
                        window.location.href = response.data.redirectUrl
                    }

                    Object.keys(response.data.errors).forEach(fieldName => {
                        const errorMessage = response.data.errors[fieldName].join('\n');
                        const input = document.querySelector(`[newName="${fieldName}"]`);
                        if (input) {
                            failValidation(input, errorMessage)
                        }
                    });
                }
            })
            .catch(error => {
                alertMsj('Ocurrió un error inesperado. Intentelo más tarde')
                console.log("Error atrapado:", error);
            });
    }

})