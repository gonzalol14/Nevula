var btnsAdministrator = document.querySelectorAll('.btn-administrator')


// Agrega el eventListener a cada botón de administrador
btnsAdministrator.forEach(btn => {
    btn.addEventListener('click', () => {
        const idUser = btn.id.split("-")[1]
        if (!btn.classList.contains('administrator')) {
            //No es admin
            axios.get(`/Admin/AdminUser?IdUser=${idUser}&isAdmin=false`)
                .then(response => {
                    // Manejar la respuesta exitosa aquí
                    console.log(response.data)
                    if (response.data.success) {
                        alertMsj('Rol de administrador agregado', 'success', 300)
                        //Cambiar estilos
                        btn.classList.add('administrator')
                        btnVerify = document.getElementById(`btn_account_verify-${idUser}`)
                        if (btnVerify.classList.contains('verified')) btnVerify.classList.remove('verified')
                    } else {
                        alertMsj(response.data.error, 'error', 1500)
                    }
                })
                .catch(error => {
                    alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                    console.log("Error atrapado:", error);
                });
        } else {
            //Es admin
            axios.get(`/Admin/AdminUser?IdUser=${idUser}&isAdmin=true`)
                .then(response => {
                    // Manejar la respuesta exitosa aquí
                    console.log(response.data)
                    if (response.data.success) {
                        alertMsj('Rol de administrador eliminado', 'success', 300)
                        //Cambiar estilos
                        btn.classList.remove('administrator')
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