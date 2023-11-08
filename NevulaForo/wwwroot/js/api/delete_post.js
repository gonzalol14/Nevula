var currentURL = window.location.href.split('?');
var pathSegments = currentURL[0].split('/').slice(3).join('/');

if (pathSegments == 'Publication/Index') {
    const deletePost = document.getElementById('delete_post')

    deletePost.addEventListener('click', (event) => {
        const idPublication = currentURL[1].split("=")[1]

        axios.get(`/Publication/DeleteApi?IdPublication=${idPublication}`)
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    alertMsj('Publicación eliminada con éxito', 'success', 300)
                    setTimeout(() => {
                        window.location.href = response.data.redirectUrl;
                    }, 300)
                } else {
                    alertMsj(response.data.error, 'error', 1500)
                }
            })
            .catch(error => {
                alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                console.log("Error atrapado:", error);
            });
    });

} else {
    const deletePosts = document.querySelectorAll('.post__delete')

    deletePosts.forEach((deletePost) => {
        deletePost.addEventListener('click', (event) => {
            const idPublication = deletePost.id.split('-')[1]

            axios.get(`/Publication/DeleteApi?IdPublication=${idPublication}`)
                .then(response => {
                    // Manejar la respuesta exitosa aquí
                    console.log(response.data)
                    if (response.data.success) {
                        alertMsj('Publicación eliminada con éxito', 'success')
                        deletePost.parentElement.parentElement.parentElement.remove()
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
}