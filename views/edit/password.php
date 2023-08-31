<section class="container__section">
    <h2 class="title__edit">Cambiar contraseña</h2>
    <form action="../controllers/edit_password.php?id=<?php echo $_SESSION['usuario']['id'] ?>" method="post">
        <div class="form-group">
            <label for="password" class="input-title">Contraseña actual</label>
            <div class="container__password">
                <input class="pass" id="password" type="password" name="currentpass">
                <button type="button" class="btn-view-password toggle-password" toggle="#password">
                    <span class="icon"><i class="bi bi-eye-fill"></i></span>
                </button>

            </div>
            <div class="msj_error">
                <?php echo (isset($message['error_oldpass']) ? $message['error_oldpass'] : ''); ?>
            </div>
        </div>

        <div class="form-group">
            <label for="password" class="input-title">Nueva contraseña</label>
            <div class="container__password">
                <input class="pass" id="newpassword" type="password" name="newpass">
                <button type="button" class="btn-view-password toggle-password" toggle="#newpassword">
                    <span class="icon"><i class="bi bi-eye-fill"></i></span>
                </button>
            </div>
            <div class="msj_error">
                <?php echo (isset($message['error_newpass']) ? $message['error_newpass'] : ''); ?>
            </div>
        </div>

        <div class="form-submit">
            <button type="reset" class="btn_edit-reset" id="edit_general-reset">Descartar</button>
            <button type="submit" class="btn_edit-submit" id="edit_general-submit">Guardar cambios</button>
        </div>
    </form>
</section>

<script src="../js/password.js" type="text/javascript"></script>