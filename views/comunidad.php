<header>
    <div class="container__left">
        <div class="container__logo">
            <img src="../img/logo_empresa_jotelson.png" alt="Logo empresa" width="65px">
        </div>

        <div>
            <button class="list_menu"><i class="bi bi-list" id="icon-list"></i></button>
        </div>

        <div class="container__list-buttons">
            <div class="container__btn-home">
                <a href="../controllers/home.php">INICIO</a>
            </div>
            <div>
                <a href="">NOVEDADES</a>
            </div>
            <div>
                <a href="../controllers/comunidad.php">COMUNIDAD</a>
            </div>
            <div>
                <a href="">AYUDA</a>
            </div>
            <div class="list-menu-profile">
                <a href="">PERFIL</a>
            </div>

        </div>
    </div>
    <div class="container__right">
        <div class="container__profile">
            <button class="btn-profile">
                <i class="bi bi-person-circle"></i>
            </button>

        </div>

        <div class="container__download">
            <button class="btn-download">DESCARGAR</button>
        </div>
    </div>
</header>

<main class="container">

    <article class="menu__comunnity">
        <!-- Pantalla +768px -->
        <a href="" class="btn__create-post-text">
            <span>Crear publicación</span>
        </a>
        <!-- Pantalla -768px -->
        <a href="" class="btn__create-post-icon" title="Crear publicación">
            <i class="bi bi-plus-square"></i>
        </a>

        <div class="container__search-bar">
            <input type="search" placeholder="Buscar...">
            <button type="submit">
                <i class="bi bi-search"></i>
            </button>
        </div>
    </article>

    <article class="article__post">

        <div class="container__info">
            <div class="container__profile-pic">
                <a href="#">
                    <img src="../img/foto_perfil.jpg" alt="">
                </a>
            </div>

            <div class="container__username">
                <a href="#">masi_cabj</a>
            </div>

            <!-- Esto depende si esta o no verificado -->
            <div class="container__verify" id="container__verify-1">
                <span class="verify">VERIFICADO</span>
                <span class="small-verify">V</span>
                <span class="popup-verify" id="popup-verify-1">Esta persona forma parte de Jotelson</span>
            </div>

            <span class="separator__info">·</span>

            <div class="container__date">
                <span>hace 7h</span>
            </div>
        </div>

        <div class="container__content-post">
            <h2 class="title--post">Problema en la instalación</h2>
            <p class="content--post">Hola, necesito ayuda con la instalación del juego, cuando intento instalarlo, luego de su descarga, me tira un error el windows defender, busque en google y no encuentro ninguna solucion, tampoco vi que en foro lo hayan comentado. Si alguien puede decirme la solucion o ayudarme estaria muy agradecido. Saludos</p>
            <div class="container__imgs"></div>
        </div>

        <div class="container__feedback">
            <a href="#" class="feedback__like"><i class="bi bi-heart"></i> 19</a>
            <a href="#" class="feedback__comments"><i class="bi bi-chat"></i> 54</a>
            <a href="#" class="feedback__share"><i class="bi bi-share"></i> Compartir</a>
        </div>

    </article>

</main>

<script src="../js/main.js" type="text/javascript"></script>
<script src="../js/comunidad.js" type="text/javascript"></script>