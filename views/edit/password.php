<section class="container__section">
    <h2 class="title__edit">Cambiar contraseña</h2>
    <form action="" method="post">
        <div class="form-group">
            <label for="password" class="input-title">Contraseña actual</label>
            <div class="container__password">
                <input class="pass" id="password" type="password" name="currentpass">
                <button type="button" class="btn-view-password toggle-password" toggle="#password">
                    <span class="icon"><i class="bi bi-eye-fill"></i></span>
                </button>
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
        </div>

        <div class="form-submit">
            <button type="reset" class="" id="edit_general-reset">DESCARTAR</button>
            <button type="submit" class="" id="edit_general-submit">GUARDAR CAMBIOS</button>
        </div>
    </form>
</section>

<script src="../js/password.js" type="text/javascript"></script>