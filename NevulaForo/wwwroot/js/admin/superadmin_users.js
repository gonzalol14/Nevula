var btnsAdministrator = document.querySelectorAll('.btn-administrator')
var btnsSuperAdministrator = document.querySelectorAll('.btn-super-administrator')


// Agrega el eventListener a cada botón de administrador
btnsAdministrator.forEach(btn => {
    btn.addEventListener('click', () => {
        const idUser = btn.id.split("-")[1]
        const username = document.getElementById(`username_${idUser}`).innerHTML

        if (!btn.classList.contains('administrator')) {
            crearModal(`¿Desea darle admin a ${username}?`, `Si le da el admin al usuario ${username}, este tendrá el poder de administrar Nevula`)
        } else {
            crearModal(`¿Desea darle admin a ${username}?`, `Si le saca el admin al usuario ${username}, este perdera el poder de administrar Nevula`)
        }

        document.getElementById('modal-confirm').onclick = function () {
            // Cerrar el modal
            document.querySelector('.modal').remove()

            if (!btn.classList.contains('administrator')) {
                //No es admin
                axios.get(`/Admin/AdminUser?IdUser=${idUser}`)
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
                axios.get(`/Admin/AdminUser?IdUser=${idUser}`)
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
        };
    });
});

btnsSuperAdministrator.forEach(btn => {
    btn.addEventListener('click', () => {
        const idUser = btn.id.split("-")[1]
        const username = document.getElementById(`username_${idUser}`).innerHTML

        if (!btn.classList.contains('super-administrator')) {
            crearModal(`¿Desea darle super-admin a ${username}?`, `Si le da el super-admin al usuario ${username}, este tendrá los mismo poderes que usted en Nevula`)
        } else {
            crearModal(`¿Desea darle super-admin a ${username}?`, `Si le saca el super-admin al usuario ${username}, este perdera el poder de super-admin en Nevula`)
        }

        document.getElementById('modal-confirm').onclick = function () {
            // Cerrar el modal
            document.querySelector('.modal').remove()

            if (!btn.classList.contains('super-administrator')) {
                //No es admin
                axios.get(`/Admin/SuperAdminUser?IdUser=${idUser}`)
                    .then(response => {
                        // Manejar la respuesta exitosa aquí
                        console.log(response.data)
                        if (response.data.success) {
                            alertMsj('Rol de super-administrador agregado', 'success', 300)
                            //Cambiar estilos
                            btn.classList.add('super-administrator')

                            document.getElementById(`btn_account_verify-${idUser}`).classList.remove('verified')
                            document.getElementById(`btn_account_admin-${idUser}`).classList.remove('administrator')
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
                axios.get(`/Admin/SuperAdminUser?IdUser=${idUser}`)
                    .then(response => {
                        // Manejar la respuesta exitosa aquí
                        console.log(response.data)
                        if (response.data.success) {
                            alertMsj('Rol de super-administrador eliminado', 'success', 300)
                            //Cambiar estilos
                            btn.classList.remove('super-administrator')
                        } else {
                            alertMsj(response.data.error, 'error', 1500)
                        }
                    })
                    .catch(error => {
                        alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                        console.log("Error atrapado:", error);
                    });
            }
        };
    });
});