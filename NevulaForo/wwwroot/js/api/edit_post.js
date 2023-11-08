const formEditPost = document.getElementById('formEditPost')

const submitButton = document.getElementById('submit_edit-post');
const originalFormValues = {};
formEditPost.querySelectorAll('input, textarea').forEach(input => {
    originalFormValues[input.name] = input.value;
});

formEditPost.addEventListener('input', function () {
    let formChanged = false;

    // Verifica si algún valor ha cambiado
    formEditPost.querySelectorAll('input, textarea').forEach(element => {
        if (originalFormValues[element.name] !== element.value) {
            formChanged = true;
        }
    });

    // Habilita/deshabilita el botón de envío según si el formulario ha cambiado
    submitButton.disabled = !formChanged;
});

formEditPost.addEventListener('submit', (event) => {
    event.preventDefault()

    const title = document.getElementById('title');
    const content = document.getElementById('description');
    //const idPublication = document.getElementById('id');

    var validForm = 0;

    //TITULO
    if (!title.value.trim()) {
        failValidation(title, 'Debe ingresar un título.')

    } else if (title.value.length < 5 || title.value.length > 250) {
        failValidation(title, 'El título debe tener entre 5 y 250 caracteres.')

    } else {
        successValidation(title)
        validForm++
    }

    //CONTENIDO
    if (!content.value.trim()) {
        failValidation(content, 'Debe ingresar una descripción.')

    } else if (content.value.length < 5 || content.value.length > 1750) {
        failValidation(content, 'El contenido del post tener entre 30 y 1750 caracteres.')

    } else {
        successValidation(content)
        validForm++
    }

    //CAMBIOS
    if (originalFormValues["Title"] != title.value || originalFormValues["Description"] != content.value) {
        validForm++
    }

    if (validForm == 3) {
        const titleValue = title.value.trim() || null
        const contentValue = content.value.trim() || null
        //const idPublicationValue = idPublication.value.trim() || null

        axios.post('/Publication/EditApi', { Title: titleValue, Description: contentValue })
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    alertMsj('Publicacion actualizada correctamente', 'success', 300)
                    setTimeout(() => {
                        window.location.href = response.data.redirectUrl;
                    }, 300)
                } else {
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
                alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                console.log("Error atrapado:", error);
            });
    }

})
