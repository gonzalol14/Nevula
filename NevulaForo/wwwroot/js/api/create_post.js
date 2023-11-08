const formCreatePost = document.getElementById('formCreatePost')

formCreatePost.addEventListener('submit', (event) => {
    event.preventDefault()

    const title = document.getElementById('title');
    const content = document.getElementById('description');

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

        axios.post('/Publication/CreateApi', { Title: titleValue, Description: contentValue })
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    alertMsj('Publicación creada con éxito', 'success', 300)
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
