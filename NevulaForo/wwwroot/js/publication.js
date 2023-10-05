
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



// Función para manejar el clic en los enlaces de "Responder"
function handleReplyLinkClick(event) {
    // Obtener el ID del comentario desde el identificador del enlace
    const commentId = event.target.getAttribute("data-comment-id");
    const commentUsername = event.target.getAttribute("data-comment-username");

    // Actualizar el valor del campo IdFatherComment en el formulario de comentarios
    const commentForm = document.getElementById("form__post-comment");
    
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
            const commentForm = document.getElementById("form__post-comment");
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