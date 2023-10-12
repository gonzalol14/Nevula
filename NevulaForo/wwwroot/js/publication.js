
// Función para manejar el clic en los enlaces de comentarios
function handleCommentLinkClick(event) {
    // Obtener el ID del comentario desde el atributo href
    const commentId = event.target.getAttribute("href").substring(1);

    // Realizar acciones específicas, como modificar el estilo CSS
    const commentElement = document.getElementById(commentId);
    if (commentElement) {
        // Agregar clase para iniciar la animación
        commentElement.classList.remove("highlighted-none");
        commentElement.classList.add("highlighted");

        // Scroll hacia la posición del comentario
        commentElement.scrollIntoView({ behavior: "smooth", block: "start" });

        // Obtener la altura del header
        const headerHeight = document.getElementById("header").offsetHeight;

        // Scroll hacia la posición del comentario ajustando por la altura del header
        window.scrollTo({
            top: commentElement.offsetTop - headerHeight - 10,
            behavior: "smooth"
        });

        // Después de 2 segundos, quitar la clase para detener la animación
        setTimeout(() => {
            commentElement.classList.remove("highlighted");
            commentElement.classList.add("highlighted-none");
        }, 1000);
    }

    // Evitar que el enlace lleve a otra página
    event.preventDefault();
}

// Asignar la función a todos los enlaces de comentarios
const commentLinks = document.querySelectorAll("a[href^='#comment']");
commentLinks.forEach(link => {
    link.addEventListener("click", handleCommentLinkClick);
});


var commentId = "";
var commentUsername = "";
var commentDescription = "";
// Función para manejar el clic en los enlaces de "Responder"
function handleReplyLinkClick(event) {
    // Obtener el ID del comentario desde el identificador del enlace
    commentId = event.target.getAttribute("data-comment-id");
    commentUsername = event.target.getAttribute("data-comment-username");
    commentDescription = document.getElementById(`comment_description-${commentId}`).innerText

    // Actualizar el valor del campo IdFatherComment en el formulario de comentarios
    const commentForm = document.getElementById("formCreateComment");
    
    if (commentForm) {
        
        const idFatherCommentInput = commentForm.querySelector("input[name='IdFatherComment']");
        if (idFatherCommentInput) {
            idFatherCommentInput.value = commentId;

            commentForm.parentNode.classList.add("active_answer")

            if (!document.getElementById('quoted_comment_alert')) {
                const alert = `<div class="quoted_comment_alert" id="quoted_comment_alert">
                                    <p>En respuesta a <span id="username_quoted_comment">@${commentUsername}<span></p>
                                    <button type="button" id="close_quoted_alert"><i class="bi bi-x-lg"></i></button>
                                </div>`

                $(commentForm.parentNode).before(alert)

                document.getElementById("Description").focus()

                const buttonCloseAlert = document.getElementById('close_quoted_alert')
                closeQuotedCommentAlert(buttonCloseAlert)
            } else {
                document.getElementById("Description").focus()
                document.getElementById('username_quoted_comment').innerText = `@${commentUsername}`
            }
        }
    }

    // Otros pasos que puedas necesitar realizar al hacer clic en "Responder"

    // Evitar que el enlace lleve a otra página
    event.preventDefault();
}

// Asignar la función a todos los enlaces de "Responder"
const replyLinks = document.querySelectorAll(".feedback__aswer-comments");
replyLinks.forEach(link => {
    link.addEventListener("click", handleReplyLinkClick);
});

function closeQuotedCommentAlert(buttonCloseAlert) {
    buttonCloseAlert.addEventListener('click', e => {
        const alert = document.getElementById('quoted_comment_alert')

        if (alert) {
            const commentForm = document.getElementById("formCreateComment");
            const idFatherCommentInput = commentForm.querySelector("input[name='IdFatherComment']");
            idFatherCommentInput.value = null;
            $(alert).remove()

            commentForm.parentNode.classList.remove("active_answer")
        }
    })
}


function isElementInViewport(element) {
    const rect = element.getBoundingClientRect();
    const windowHeight = window.innerHeight || document.documentElement.clientHeight;
    const windowWidth = window.innerWidth || document.documentElement.clientWidth;

    return (
        rect.top >= 0 &&
        rect.left >= 0 &&
        rect.bottom <= windowHeight &&
        rect.right <= windowWidth
    );
}




/* Mismo que en create_comment.js, pero sin recarga de pagina, falta que se actualicen los eventListener (paja)
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

                    const titleAnswer = document.getElementById('title_answer')
                    const comment = response.data.comment //header__profile-pic

                    var newComment =
                        `<div class="container__comment" id="comment_${comment.Id}">
                            <div class="container__info">
                                <div class="comment__profile-pic">
                                    <a asp-route-IdUser="${comment.IdUser}" asp-controller="Account" asp-action="Index">
                                        <img src="${document.getElementById('header__profile-pic').getAttribute('src')}" alt="">
                                    </a>
                                </div>

                                <div class="container__username">
                                    <a asp-route-IdUser="${comment.IdUser}" asp-controller="Account" asp-action="Index">${response.data.username}</a>
                                </div>`

                    if (response.data.userIdRole == 2) {
                        newComment +=
                            `< !--Esto depende si esta o no verificado -->
                                        <div class="container__verify" id="container__verify-2">
                                            <span class="verify">VERIFICADO</span>
                                            <span class="small-verify">V</span>
                                            <span class="popup-verify" id="popup-verify-2">Esta persona forma parte de Nevula</span>
                                        </div>`
                    }

                    newComment +=
                        `<span class="separator__info">·</span>

                                    <div class="container__date">
                                        <span>${response.data.stylizeDate}</span>
                                    </div>
                                </div>

                                <div class="container__answer">`

                    if (comment.IdFatherComment != null) {
                        newComment +=
                            `<div class="container__content-cited">
                                            <p class="title--cited">Comentario de <a class="username_quoted" href="#comment_${commentId}">@${commentUsername}</a></p>
                                            <p class="content--cited">${commentDescription}</p>
                                        </div>`
                    }

                    newComment +=
                        `<div class="container__content-post">
                                        <p class="">${comment.Description}</p>
                                        <div class="container__imgs"></div>
                                    </div>
                                </div>

                                <div class="container__feedback">
                                    <div>
                                        <a href="#" class="feedback__aswer-comments" id="answer_comment_${comment.Id}" data-comment-id="${comment.Id}" data-comment-username="${response.data.username}">Responder</a>
                                    </div>
                                    <div>
                                        <a class="post__delete" asp-route-IdComment="${comment.Id}" asp-controller="Comment" asp-action="Delete"><i class="bi bi-trash3"></i> </a>
                                    </div>
                                </div>
                            </div>`

                    titleAnswer.insertAdjacentHTML('afterend', newComment);
                    comment.value = "";

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
};*/