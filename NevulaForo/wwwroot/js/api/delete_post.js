const deletePosts = document.querySelectorAll('.post__delete')

deletePosts.forEach((deletePost) => {
    deletePost.addEventListener('click', (event) => {
        const idPublication = deletePost.id.split('-')[1]

        axios.get(`/Publication/DeleteApi?IdPublication=${idPublication}`)
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    console.log("Publicación eliminada correctamente")
                    deletePost.parentElement.parentElement.parentElement.remove()
                }
            })
            .catch(error => {
                console.log("Error atrapado:", error);
            });
    })
});