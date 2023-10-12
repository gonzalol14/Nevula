const formEditPost = document.getElementById('formEditPost')

formEditPost.addEventListener('submit', (event) => {
    event.preventDefault()

    const title = document.getElementById('title');
    const content = document.getElementById('description');
    const idPublication = document.getElementById('id');

    var validForm = 0;

    if (!title.value.trim()) {
        failValidation(title, 'Debe ingresar un título.')

    } else if (title.value.length < 5 || title.value.length > 250) {
        failValidation(title, 'El título debe tener entre 5 y 250 caracteres.')

    } else {
        successValidation(title)
        validForm++
    }

    if (!content.value.trim()) {
        failValidation(content, 'Debe ingresar una descripción.')

    } else if (content.value.length < 5 || content.value.length > 1750) {
        failValidation(content, 'El contenido del post tener entre 30 y 1750 caracteres.')

    } else {
        successValidation(content)
        validForm++
    }

    if (validForm == 2) {
        const titleValue = title.value.trim() || null
        const contentValue = content.value.trim() || null
        const idPublicationValue = idPublication.value.trim() || null

        axios.post('/Publication/EditApi', { Title: titleValue, Description: contentValue, Id: idPublicationValue })
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    window.location.href = response.data.redirectUrl; // Redirigir a su perfil
                } else {
                    Object.keys(response.data.errors).forEach(fieldName => {
                        const errorMessage = response.data.errors[fieldName].join('\n');
                        console.log(`${fieldName}: ${errorMessage}`);
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
    const element = document.querySelector(`[data-valmsg-for="${input.getAttribute('name')}"]`);
    element.classList.remove("field-validation-valid");
    element.classList.add("field-validation-error");

    input.classList.add('input-validation-error')

    element.innerText = msj;
};
const successValidation = (input) => {
    const element = document.querySelector(`[data-valmsg-for="${input.getAttribute('name')}"]`);
    element.classList.remove("field-validation-error");
    element.classList.add("field-validation-valid");

    input.classList.remove('input-validation-error')

    element.innerText = null;
};