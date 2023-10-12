const formEditGeneral = document.getElementById('formEditGeneral')

formEditGeneral.addEventListener('submit', (event) => {
    event.preventDefault()

    const name = document.getElementById('name');
    const surname = document.getElementById('surname');
    const username = document.getElementById('username');
    const email = document.getElementById('email');
    const description = document.getElementById('description');

    var validForm = 0;
    
    //NOMBRE
    if (name.value.trim() && (name.value.length < 3 || name.value.length > 30)) {
        failValidation(name, 'El nombre debe tener entre 3 y 30 caracteres.')
    } else {
        successValidation(name)
        validForm++
    }

    //APELLIDO
    if (surname.value.trim() && (surname.value.length < 3 || surname.value.length > 30)) {
        failValidation(surname, 'El apellido debe tener entre 3 y 30 caracteres.')
    } else {
        successValidation(surname)
        validForm++
    }

    //EMAIL
    if (!email.value.trim()) {
        failValidation(email, 'Debe ingresar un correo electrónico.')

    } else if (!emailValidation(email.value)) {
        failValidation(email, 'Debe ingresar un correo electrónico válido.')

    } else if (email.value.length > 50) {
        failValidation(email, 'Correo electronico demasiado largo.')

    } else {
        successValidation(email)
        validForm++
    }

    //USERNAME
    if (!username.value.trim()) {
        failValidation(username, 'Debe ingresar un nombre de usuario.')

    } else if (username.value.length < 3 || username.value.length > 30) {
        failValidation(username, 'El nombre de usuario debe tener entre 3 y 30 caracteres.')

    } else {
        successValidation(username)
        validForm++
    }

    //DESCRIPCION
    if (description.value.trim() && description.value.length > 250) {
        failValidation(description, 'La descripción debe tener hasta 250 caracteres.')

    } else {
        successValidation(description)
        validForm++
    }

    if (validForm == 5) {
        const nameValue = name.value.trim() || null
        const surnameValue = surname.value.trim() || null
        const usernameValue = username.value.trim() || null
        const emailValue = email.value.trim() || null
        const descriptionValue = description.value.trim() || null

        axios.post('/Account/EditGeneral', { Name: nameValue, Surname: surnameValue, Username: usernameValue, Email: emailValue, Description: descriptionValue })
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    console.log('Datos actualizados correctamente')
                    //window.location.href = response.data.redirectUrl; // Redirigir a la página de iniciar sesion
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

    input.classList.add('input-validation-error')

    element.innerText = msj;
};
const successValidation = (input) => {
    const element = document.querySelector(`[data-valmsg-for="${input.getAttribute('name')}"]`)
    element.classList.remove("field-validation-error");
    element.classList.add("field-validation-valid");

    input.classList.remove('input-validation-error')

    element.innerText = null;
};

const emailValidation = (email) => {
    return /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,3}$/i.test(email);
}