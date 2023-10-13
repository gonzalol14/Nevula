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
                    alertMsj('Publicación eliminada con éxito', 300)
                    setTimeout(() => {
                        window.location.href = response.data.redirectUrl;
                    }, 300)
                }
            })
            .catch(error => {
                alertMsj('Ocurrió un error inesperado. Intentelo más tarde')
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
                        alertMsj('Publicación eliminada con éxito')
                        deletePost.parentElement.parentElement.parentElement.remove()
                    }
                })
                .catch(error => {
                    alertMsj('Ocurrió un error inesperado. Intentelo más tarde')
                    console.log("Error atrapado:", error);
                });
        })
    });
}