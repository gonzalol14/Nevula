var btnsDeleteComment = document.querySelectorAll('.btn-delete')
btnsDeleteComment.forEach(btnDeleteComment => {
    btnDeleteComment.addEventListener('click', () => {
        const IdComment = btnDeleteComment.id.split("-")[1]
        console.log(IdComment)
        axios.get(`/Admin/DeleteComment?IdComment=${IdComment}`)
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    alertMsj('El comentario ha sido eliminado con éxito', 'success', 300)
                    //Cambiar estilos
                    btnDeleteComment.parentNode.parentNode.parentNode.remove()
                } else {
                    alertMsj(response.data.error, 'error', 1500)
                }
            })
            .catch(error => {
                alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                console.log("Error atrapado:", error);
            });
    })
})