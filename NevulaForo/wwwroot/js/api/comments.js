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

        axios.post('/Comment/CreateApi', { Description: commentValue, IdPublication: idPublicationValue, IdFatherComment: idFatherCommentValue })
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    //Data totalmente dinamico en publication.js, falta que se actualicen los eventListener, pero paja
                    location.reload();
                } else {
                    Object.keys(response.data.errors).forEach(fieldName => {
                        const errorMessage = response.data.errors[fieldName].join('\n');
                        failValidation(comment, errorMessage)
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



const deleteComments = document.querySelectorAll('.comment__delete')

deleteComments.forEach((deleteComment) => { 
    deleteComment.addEventListener('click', (event) => {
        const idComment = deleteComment.id.split('-')[1]

        axios.get(`/Comment/DeleteApi?IdComment=${idComment}`)
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    console.log("Comentario eliminado correctamente")
                    const spanCantComments = document.getElementById('span_cant-comments')

                    let cantComments = spanCantComments.innerText.split(" ")[0]
                    cantComments = (parseInt(cantComments) - 1 > 0) ? parseInt(cantComments) - 1 : 0

                    if (cantComments != 1) {
                        spanCantComments.innerText = `${cantComments} Comentarios`
                    } else {
                        spanCantComments.innerText = `${cantComments} Comentario`
                    }

                    deleteComment.parentElement.parentElement.parentElement.remove()
                } 
            })
            .catch(error => {
                console.log("Error atrapado:", error);
            });
    })
});