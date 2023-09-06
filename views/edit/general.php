<section class="container__section">
    <h2 class="title__edit">Ajustes generales</h2>

    <form action="../controllers/edit_general.php?id=<?php echo $_SESSION['usuario']['id'] ?>" method="post">
        <div class="form-group">
            <label for="" class="input-title">Nombre de usuario</label>
            <input type="text" name="username" class="" value="<?php echo $_SESSION['usuario']['nombre_usuario'] ?>">
            <div class="msj_error">
                <?php echo (isset($message['error_username']) ? $message['error_username'] : ''); ?>
            </div>
        </div>

        <div class="form-group">
            <label for="email" class="input-title">Dirección de correo</label>
            <input type="email" name="email" class="" value="<?php echo $_SESSION['usuario']['email'] ?>">
            <div class="msj_error">
                <?php echo (isset($message['error_email']) ? $message['error_email'] : ''); ?>
            </div>
        </div>

        <div class="container__full-name">
            <div class="form-group container__name">
                <label for="name" class="input-title">Nombre</label>
                <input type="text" name="name" class="" value="<?php echo $_SESSION['usuario']['nombre'] ?>">
                <div class="msj_error">
                    <?php echo (isset($message['error_name']) ? $message['error_name'] : ''); ?>
                </div>
            </div>

            <div class="form-group container__surname">
                <label for="surname" class="input-title">Apellido</label>
                <input type="text" name="surname" class="" value="<?php echo $_SESSION['usuario']['apellido'] ?>">
                <div class="msj_error">
                    <?php echo (isset($message['error_surname']) ? $message['error_surname'] : ''); ?>
                </div>
            </div>
        </div>


        <div class="form-group">
            <label for="" class="input-title">Descripción</label>
            <textarea name="description" id="" value="<?php echo $_SESSION['usuario']['descripcion'] ?>" maxlength="250"></textarea>
            <div class="msj_error">
                <?php echo (isset($message['error_description']) ? $message['error_description'] : ''); ?>
            </div>
        </div>

        <div class="form-submit">
            <button type="reset" class="btn_edit-reset" id="edit_general-reset">Descartar</button>
            <button type="submit" class="btn_edit-submit" id="edit_general-submit">Guardar cambios</button>
        </div>
    </form>

</section>