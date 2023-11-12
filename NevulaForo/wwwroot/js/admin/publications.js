var btnsDeletePublication = document.querySelectorAll('.btn-delete')

// Agrega el eventListener a cada botón de eliminar cuenta
btnsDeletePublication.forEach(btn => {
    btn.addEventListener('click', () => {
        const idPost = btn.id.split("-")[1]
        if (!btn.classList.contains('banned')) {
            axios.get(`/Admin/DeletePublication?IdPublication=${idPost}`)
                .then(response => {
                    // Manejar la respuesta exitosa aquí
                    console.log(response.data)
                    if (response.data.success) {
                        alertMsj('La publicación ha sido baneada con éxito', 'success', 300)
                        //Cambiar estilos
                        btn.parentNode.parentNode.parentNode.remove()
                    } else {
                        alertMsj(response.data.error, 'error', 1500)
                    }
                })
                .catch(error => {
                    alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                    console.log("Error atrapado:", error);
                });
        } else {
            alertMsj('La publicación ya se encuentra baneada', 'error', 1500)
        }
    });
});