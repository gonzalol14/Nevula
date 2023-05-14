<header>
    <div class="container__left">
        <div class="container__logo">
            <img src="../img/logo_empresa_jotelson.png" alt="Logo empresa" width="65px">
        </div>

        <div>
            <button class="list_menu"><i class="bi bi-list" id="icon-list"></i></button>
        </div>

        <ul class="container__list-buttons">
            <li class="container__btn-home">
                <a href="">INICIO</a>
            </li>
            <li>
                <a href="">NOVEDADES</a>
            </li>
            <li>
                <a href="../controllers/comunidad.php">COMUNIDAD</a>
            </li>
            <li>
                <a href="">AYUDA</a>
            </li>
            <li class="list-menu-profile">
                <a href="">PERFIL</a>
            </li>

        </ul>
    </div>

    <div class="container__right">
        <div class="container__profile">
            <a href="../controllers/perfil.php" class="btn-profile">
                <i class="bi bi-person-circle"></i>
            </a>

        </div>

        <div class="container__download">
            <button id="btnDescarga" class="btn-download" onclick="descargarArchivo()" >DESCARGAR</button>
        </div>
    </div>
</header>

<script src="../js/main.js" type="text/javascript"></script>