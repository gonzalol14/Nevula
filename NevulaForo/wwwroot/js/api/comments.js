const formCreateComment = document.getElementById('formCreateComment')

formCreateComment.addEventListener('submit', (event) => {
    event.preventDefault()

    const comment = document.getElementById('Description');
    const idPublication = document.getElementById('IdPublication');
    const idFatherComment = document.getElementById('IdFatherComment');

    var validForm = 0;

    if (!comment.value.trim()) {
        failValidation(comment, 'Debe ingresar un comentario.')

    } else if (comment.value.length > 300) {
        failValidation(comment, 'El comentario debe tener hasta 300 caracteres.')

    } else {
        successValidation(comment)
        validForm++
    }

    if (validForm == 1) {
        const commentValue = comment.value.trim() || null
        const idPublicationValue = idPublication.value || null
        const idFatherCommentValue = idFatherComment.value || null

        axios.post('/Comment/CreateApi', { Description: commentValue, IdPublication: idPublicationValue,IdFatherComment: idFatherCommentValue })
            .then(response => {
                // Manejar la respuesta exitosa aquí
                if (response.data.success) {
                    //Data totalmente dinamico en publication.js, falta que se actualicen los eventListener, pero paja
                    alertMsj('Comentario publicado con éxito', 'success', 300)
                    setTimeout(() => {
                        location.reload();
                    }, 300)

                } else {
                    Object.keys(response.data.errors).forEach(fieldName => {
                        const errorMessage = response.data.errors[fieldName].join('\n');

                        if (fieldName == 'errorGeneral') {
                            alertMsj(errorMessage, 'error')
                        } else {
                            failValidation(comment, errorMessage)
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

const deleteComments = document.querySelectorAll('.comment__delete')

deleteComments.forEach((deleteComment) => { 
    deleteComment.addEventListener('click', (event) => {
        const idComment = deleteComment.id.split('-')[1]

        axios.get(`/Comment/DeleteApi?IdComment=${idComment}`)
            .then(response => {
                // Manejar la respuesta exitosa aquí
                if (response.data.success) {
                    deleteComment.parentElement.parentElement.parentElement.remove()

                    alertMsj('Comentario eliminado con éxito', 'success')

                    const spanCantComments = document.getElementById('span_cant-comments')

                    let cantComments = spanCantComments.innerText.split(" ")[0]
                    cantComments = (parseInt(cantComments) - 1 > 0) ? parseInt(cantComments) - 1 : 0

                    if (cantComments != 1) {
                        spanCantComments.innerText = `${cantComments} Comentarios`
                    } else {
                        spanCantComments.innerText = `${cantComments} Comentario`
                    }
                } else {
                    alertMsj(response.data.error, 'error', 1500)
                }
            })
            .catch(error => {
                alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                console.log("Error atrapado:", error);
            });
    })
});