<header>
    <div class="container__left">
        <div class="container__logo">
            <img src="../img/logo sin letras.png" alt="Logo empresa" width="65px">
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
            <li class="container_dropdown" id="container_dropdown-community">
                <a href="comunidad.php" class="">COMUNIDAD</a>
                <!-- Proximamente... -->
                <ul class="container__dropdown--header dropdown dropdown-community">
                    <li><a href="comunidad.php">FORO</a></li>
                    <li><a href="post_publication.php">CREAR PUBLICACIÓN</a></li>
                </ul>

            </li>
            <li>
                <a href="../controllers/ayuda.php">AYUDA</a>
            </li>
            <li class="list-menu-profile container_dropdown" id="container_dropdown-profile1">
                <a href="profile.php" class="">PERFIL</a>
                <!-- Proximamente... -->
                <ul class="container__dropdown--header dropdown dropdown-profile1">
                    <li><a href="profile.php">VER PERFIL</a></li>
                    <li><a href="edit_general.php">EDITAR PERFIL</a></li>
                    <li><a href="">CERRAR SESIÓN</a></li>
                </ul>

            </li>
            <li class="list-menu-search">
                <div class="container__search-bar">
                    <input type="search" placeholder="Buscar...">
                    <button type="submit">
                        <a class="bi bi-search" href="../controllers/search.php"></a>
                    </button>
                </div>
            </li>

        </ul>
    </div>

    <div class="container__right">
        <div class="container__profile container_dropdown" id="container_dropdown-profile2">
            <a href="../controllers/profile.php" class="btn-profile">
                <i class="bi bi-person-circle"></i>
            </a>
            <!-- Proximamente... -->
            <ul class="container__dropdown--header dropdown dropdown-profile2">
                <li><a href="profile.php">VER PERFIL</a></li>
                <li><a href="edit_general.php">EDITAR PERFIL</a></li>
                <li><a href="">CERRAR SESIÓN</a></li>
            </ul>
        </div>

        <div class="container__download">
            <button id="btnDescarga" class="btn-download" onclick="descargarArchivo()">DESCARGAR</button>
        </div>
    </div>
</header>