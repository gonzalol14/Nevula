const formRegister = document.getElementById('formRegister')

formRegister.addEventListener('submit', (event) => {
    event.preventDefault()

    const name = document.getElementById('name');
    const surname = document.getElementById('surname');
    const username = document.getElementById('username');
    const email = document.getElementById('email');
    const password = document.getElementById('password');

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

    //CONTRASEÑA
    if (!password.value.trim()) {
        failValidation(password.parentElement, 'Debe ingresar una contraseña.', 'Password')

    } else if (password.value.length < 3 || password.value.length > 50) {
        failValidation(password.parentElement, 'Debe ingresar una combinación de entre 3 y 50 caracteres.', 'Password')

    } else {
        successValidation(password.parentElement, 'Password')
        validForm++
    }

    if (validForm == 5) {
        const nameValue = name.value.trim() || null
        const surnameValue = surname.value.trim() || null
        const usernameValue = username.value.trim() || null
        const emailValue = email.value.trim() || null
        const passwordValue = password.value.trim() || null

        axios.post('/Access/RegisterApi', { Name: nameValue, Surname: surnameValue, Username: usernameValue, Email: emailValue, Password: passwordValue })
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    window.location.href = response.data.redirectUrl; // Redirigir a la página de iniciar sesion
                } else {
                    Object.keys(response.data.errors).forEach(fieldName => {
                        const errorMessage = response.data.errors[fieldName].join('\n');
                        console.log(`${fieldName}: ${errorMessage}`);
                        const input = document.querySelector(`[name="${fieldName}"]`);
                        if (input) {
                            failValidation((fieldName == 'Password') ? input.parentElement : input, errorMessage, fieldName)
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

const emailValidation = (email) => {
    return /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,3}$/i.test(email);
}