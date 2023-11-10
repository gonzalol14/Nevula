var btnsVerify = document.querySelectorAll('.btn-verify')
var btnsDeleteAccount = document.querySelectorAll('.btn-delete')

// Agrega el eventListener a cada botón de verificar
btnsVerify.forEach(btn => {
    btn.addEventListener('click', () => {
        const idUser = btn.id.split("-")[1]
        if (!btn.classList.contains('verified')) {
            //No esta verificado
            axios.get(`/Admin/VerifyUser?IdUser=${idUser}&isVerified=false`)
                .then(response => {
                    // Manejar la respuesta exitosa aquí
                    console.log(response.data)
                    if (response.data.success) {
                        alertMsj('Rol de verificado eliminado', 'success', 300)
                        //Cambiar estilos
                        btn.classList.add('verified')
                        btnAdmin = document.getElementById(`btn_account_admin-${idUser}`)
                        if (btnAdmin.classList.contains('administrator')) btnAdmin.classList.remove('administrator')
                    } else {
                        alertMsj(response.data.error, 'error', 1500)
                    }
                })
                .catch(error => {
                    alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                    console.log("Error atrapado:", error);
                });
        } else {
            //Esta verificado
            axios.get(`/Admin/VerifyUser?IdUser=${idUser}&isVerified=true`)
                .then(response => {
                    // Manejar la respuesta exitosa aquí
                    console.log(response.data)
                    if (response.data.success) {
                        alertMsj('Rol de verificado eliminado', 'success', 300)
                        //Cambiar estilos
                        btn.classList.remove('verified')
                    } else {
                        alertMsj(response.data.error, 'error', 1500)
                    }
                })
                .catch(error => {
                    alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                    console.log("Error atrapado:", error);
                });
        }
    });
});


// Agrega el eventListener a cada botón de eliminar cuenta
btnsDeleteAccount.forEach(btn => {
    btn.addEventListener('click', () => {
        const idUser = btn.id.split("-")[1]
        axios.get(`/Admin/DeleteUser?IdUser=${idUser}`)
            .then(response => {
                // Manejar la respuesta exitosa aquí
                console.log(response.data)
                if (response.data.success) {
                    alertMsj('Usuario eliminado con éxito', 'success', 300)
                    //Cambiar estilos
                    btn.parentNode.parentNode.parentNode.parentNode.remove()
                } else {
                    alertMsj(response.data.error, 'error', 1500)
                }
            })
            .catch(error => {
                alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                console.log("Error atrapado:", error);
            });
    });
});