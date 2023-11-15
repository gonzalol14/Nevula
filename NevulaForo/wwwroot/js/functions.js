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

const alertMsj = (msj, type, time = 1000) => {
    const alertMsjHTML = `<div class="alert_msj msj_${type}">
                            <span>${msj}</span>
                        </div>`
    document.getElementById('header').insertAdjacentHTML('afterend', alertMsjHTML);

    setTimeout(() => {
        // Eliminar la alerta después de 1 segundo
        document.querySelector('.alert_msj').remove()
    }, time);
};


function crearModal(title, texto) {
    // Crear el modal y el contenido
    var modal = `<div class="modal" style="display: block;">
                    <div class="modal-content">
                        <div class="modal-info">
                            <h3>${title}</h3>
                            <p>${texto}</p>
                        </div>
                        <div class="modal-btns">
                            <button class="modal-btn cancel" id="modal-cancel">Cancelar</button>
                            <button class="modal-btn confirm" id="modal-confirm">Aceptar</button>
                        </div>
                    </div>
                </div>`
    document.body.insertAdjacentHTML('beforeend', modal);


    document.getElementById('modal-cancel').onclick = function () {
        console.log('Cancelar clicado');
        // Cerrar el modal
        document.querySelector('.modal').remove()
    };

    // AGREGAR LOS ADDEVENTLISTENER DEL BOTON DE CONFIRMAR
}
// Cerrar el modal si se hace clic fuera del contenido del modal
window.onclick = function (event) {
    var modals = document.getElementsByClassName('modal');
    for (var i = 0; i < modals.length; i++) {
        if (event.target == modals[i]) {
            modals[i].remove()
        }
    }
}