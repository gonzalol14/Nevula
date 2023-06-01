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
                <a href="home.php">INICIO</a>
            </li>
            <li>
                <a href="">NOVEDADES</a>
            </li>
            <li>
                <a href="comunidad.php">COMUNIDAD</a>
                <!-- Proximamente...
                <ul>
                    <li>Foro</li>
                    <li>Crear publicación</li>
                </ul>
                -->
            </li>
            <li>
                <a href="../controllers/ayuda.php">AYUDA</a>
            </li>
            <li class="list-menu-profile">
                <a href="profile.php">PERFIL</a>
                <!-- Proximamente...
                <ul>
                    <li>Ver perfil</li>
                    <li>Editar perfil</li>
                    <li>Cerrar sesión</li>
                </ul>
                -->
            </li>
            <li class="list-menu-search">
                <div class="container__search-bar">
                    <input type="search" placeholder="Buscar...">
                    <button type="submit">
                        <i class="bi bi-search"></i>
                    </button>
                </div>
            </li>

        </ul>
    </div>

    <div class="container__right">
        <div class="container__profile">
            <a href="../controllers/profile.php" class="btn-profile">
                <i class="bi bi-person-circle"></i>
            </a>

        </div>

        <div class="container__download">
            <button id="btnDescarga" class="btn-download" onclick="descargarArchivo()">DESCARGAR</button>
        </div>
    </div>
</header>