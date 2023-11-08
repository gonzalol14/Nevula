const formLogin = document.getElementById('formLogin')
const error_msj = document.getElementById('msj_error_login')

formLogin.addEventListener('submit', (event) => {
    event.preventDefault()

    const email = document.getElementById('email');
    const password = document.getElementById('password');
    const remember = document.getElementById('remember');
    var validForm = 0;

    if (!email.value.trim() || !password.value.trim()) {
        failValidation2('Debe ingresar un correo electrónico y una contraseña.')

    } else {
        successValidation2()
        validForm++
    }

    if (validForm == 1) {
        const emailValue = email.value.trim()
        const passwordValue = password.value.trim()
        const rememberValue = remember.checked

        axios.post('/Access/LoginApi', { Email: emailValue, Password: passwordValue, Remember: rememberValue })
            .then(response => {
                // Manejar la respuesta exitosa aquí
                if (response.data.success) {
                    alertMsj('Sesión iniciada con éxito', 'success', 300)
                    setTimeout(() => {
                        window.location.href = response.data.redirectUrl;
                    }, 300)
                } else {
                    failValidation2(response.data.errorMessage);
                }
            })
            .catch(error => {
                alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                console.log("Error atrapado:", error);
            });

    }
    
})

const failValidation2 = (msj) => {
    error_msj.classList.remove("field-validation-valid");
    error_msj.classList.add("field-validation-error");

    error_msj.parentElement.style.display = 'flex'

    error_msj.innerText = msj;
};
const successValidation2 = () => {
    error_msj.classList.remove("field-validation-error");
    error_msj.classList.add("field-validation-valid");

    error_msj.parentElement.style.display = 'none'

    error_msj.innerText = null;
};