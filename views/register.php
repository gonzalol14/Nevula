    <div class="container__all">
        <div class="container__data">
            <div class="container__logo">
                <img src="../img/NASA_logo.svg.webp" alt="Logo Jotelson" width="200px">
            </div>

            <div class="container__title">
                <p>Crea tu cuenta en Jotelson</p>
            </div>

            <form method="POST" action="" id="form" class="register__form">

                <div class="container__full-name">
                    <div class="form-group container__name">
                        <label for="name" class="input-title">Nombre</label>
                        <input type="text" name="name" class="">
                    </div>

                    <div class="form-group container__surname">
                        <label for="surname" class="input-title">Apellido</label>
                        <input type="text" name="surname" class="">
                    </div>
                </div>


                <div class="form-group">
                    <label for="username" class="input-title">Nombre de usuario</label>
                    <input type="text" name="username" class="">
                </div>

                <div class="form-group">
                    <label for="email" class="input-title">Dirección de correo</label>
                    <input type="email" name="email" class="">
                </div>

                <div class="form-group">
                    <label for="password" class="input-title">Contraseña</label>
                    <div class="container__password">
                        <input class="pass" id="password" type="password" name="password">
                        <button type="button" class="btn-view-password toggle-password" toggle="#password">
                            <span class="icon"><i class="bi bi-eye-fill"></i></span>
                        </button>
                    </div>
                </div>

                <div class="form-group">
                    <label class="label__remember">
                        <span class="terms-conditions">Tengo 13 años o más y estoy de acuerdo con los <a class="links" href="#terminosycondiciones">términos y condiciones</a></span>
                        <input type="checkbox" id="remember" name="remember" value="1" >
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