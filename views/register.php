    <div class="container__all">
        <div class="container__data">
            <div class="container__logo">
                <img src="../img/logo_empresa_jotelson.png" alt="Logo Nevula" width="300px">
            </div>

            <div class="container__title">
                <p>Crea tu cuenta en Nevula</p>
            </div>

            <form method="POST" action="../controllers/register.php" id="form" class="register__form">

                <div class="container__full-name">
                    <div class="form-group container__name">
                        <label for="name" class="input-title">Nombre</label>
                        <input type="text" name="name" class="<?php echo (isset($message['name']) ? 'input_error' : ''); ?>">
                        <div class="msj_error">
                            <?php echo (isset($message['name']) ? $message['name'] : ''); ?>
                        </div>
                    </div>

                    <div class="form-group container__surname">
                        <label for="surname" class="input-title">Apellido</label>
                        <input type="text" name="surname" class="<?php echo (isset($message['surname']) ? 'input_error' : ''); ?>">
                        <div class="msj_error">
                            <?php echo (isset($message['surname']) ? $message['surname'] : ''); ?>
                        </div>
                    </div>
                </div>


                <div class="form-group">
                    <label for="username" class="input-title">Nombre de usuario</label>
                    <input type="text" name="username" class="<?php echo (isset($message['username']) ? 'input_error' : ''); ?>">
                    <div class="msj_error">
                        <?php echo (isset($message['username']) ? $message['username'] : ''); ?>
                    </div>
                </div>

                <div class="form-group">
                    <label for="email" class="input-title">Dirección de correo</label>
                    <input type="email" name="email" class="<?php echo (isset($message['email']) ? 'input_error' : ''); ?>">
                    <div class="msj_error">
                        <?php echo (isset($message['email']) ? $message['email'] : ''); ?>
                    </div>
                </div>


                <div class="form-group">
                    <label for="password" class="input-title">Contraseña</label>
                    <div class="container__password">
                        <input class="pass <?php echo (isset($message['password']) ? 'input_error' : ''); ?>" id="password" type="password" name="password">
                        <button type="button" class="btn-view-password toggle-password" toggle="#password">
                            <span class="icon"><i class="bi bi-eye-fill"></i></span>
                        </button>
                        <div class="msj_error">
                            <?php echo (isset($message['password']) ? $message['password'] : ''); ?>
                        </div>
                    </div>
                </div>


                <div class="form-group">
                    <label class="label__remember">
                        <span class="terms-conditions">Tengo 13 años o más y estoy de acuerdo con los <a class="links" href="#terminosycondiciones">términos y condiciones</a></span>
                        <input type="checkbox" id="remember" name="remember" value="1">
                        <span class="checkmark"></span>
                    </label>

                </div>

                <div class="form-group">
                    <button type="submit" class="" id="register-submit">Registrarse</button>
                </div>

            </form>

            <div class="container__separator">
                <div class="separator-line">
                    <hr>
                </div>
                <div class="separator-letter">
                    <span>O</span>
                </div>
                <div class="separator-line">
                    <hr>
                </div>
            </div>

            <p class="text-create-account">¿Ya tenés cuenta? <a class="links" href="login.php">Iniciar sesión</a></p>


        </div>
    </div>

    <script src="../js/password.js" type="text/javascript"></script>