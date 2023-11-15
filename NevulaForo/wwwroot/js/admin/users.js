var btnsVerify = document.querySelectorAll('.btn-verify')
var btnsDeleteAccount = document.querySelectorAll('.btn-delete')

// Agrega el eventListener a cada botón de verificar
btnsVerify.forEach(btn => {
    btn.addEventListener('click', () => {
        const idUser = btn.id.split("-")[1]
        const username = document.getElementById(`username_${idUser}`).innerHTML

        if (!btn.classList.contains('verified')) {
            crearModal(`¿Desea verificar a ${username}?`, `Si verifica al usuario ${username}, este tendra una nueva insignia de usuario verificado`)
        } else {
            crearModal(`¿Desea sacarle el verificado a ${username}?`, `Si desverifica al usuario ${username}, este perdera su insignia de usuario verificado`)
        }

        document.getElementById('modal-confirm').onclick = function () {
            // Cerrar el modal
            document.querySelector('.modal').remove()

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
        };
    });
});


// Agrega el eventListener a cada botón de eliminar cuenta
btnsDeleteAccount.forEach(btn => {
    btn.addEventListener('click', () => {
        const idUser = btn.id.split("-")[1]
        const username = document.getElementById(`username_${idUser}`).innerHTML

        if (!btn.classList.contains('banned')) {
            crearModal(`¿Desea banear a ${username}?`, `Si baneas al usuario ${username}, este sufrira un shadowban y, además, no podra volver a loguearse hasta desbanearlo`)
        } else {
            crearModal(`¿Desea desbanear a ${username}?`, `Si desbaneas al usuario ${username}, se le sacara el shadowban y, también, podra volver a usar su cuenta normalmente`)
        }

        document.getElementById('modal-confirm').onclick = function () {
            // Cerrar el modal
            document.querySelector('.modal').remove()

            axios.get(`/Admin/DeleteUser?IdUser=${idUser}`)
                .then(response => {
                    // Manejar la respuesta exitosa aquí
                    console.log(response.data)
                    if (response.data.success) {
                        alertMsj(response.data.msj, 'success', 300)
                        //Cambiar estilos
                        if (response.data.isBanned) {
                            btn.classList.add('banned')
                        } else {
                            btn.classList.remove('banned')
                        }
                    } else {
                        alertMsj(response.data.error, 'error', 1500)
                    }
                })
                .catch(error => {
                    alertMsj('Ocurrió un error inesperado. Intentelo más tarde', 'error')
                    console.log("Error atrapado:", error);
                });
        };
        
    });
});