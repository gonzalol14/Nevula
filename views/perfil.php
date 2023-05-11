<!-- esta parte es provisional -->
<head>
    <link rel="stylesheet" href="../css/comunidad.css">
</head>
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
            <a href="../controllers/perfil.php" class="btn-profile">
                <i class="bi bi-person-circle"></i>
            </a>

        </div>

        <div class="container__download">
            <button class="btn-download">DESCARGAR</button>
        </div>
    </div>
</header>

<main class="container">
    <article class="container__perfil">
        <div class="container_info">
            <div class="container__profile-pic">
                <img src="../img/foto_perfil.jpg" alt="">
            </div>
            <div class="container_name">
               <span> masi_cabj </span>
            </div>
            <div class="container_verify" id="container__verify-1">
                <span class="verify">VERIFICADO</span>
                <span class="small-verify">V</span>
                <span class="popup-verify" id="popup-verify-1">Esta persona forma parte de Jotelson</span>
            </div>
            <div class="container__posts">
                <a href="#" class="posts"> publicaciones: </a>
                <span id="posts-id=1"> 8 </span>
            </div>
            <div class="container__followers">
                <a href="#" class="followers">seguidores:</a>
                <span id="followers-id=1">5M</span>
            </div>
            <div class="container__following">
                <a href="#" class="following"> siguiendo:</a>
                <span id="following-id=1">6</span>
            </div>
        </div>
        <div class="container__description">    
            <span>
                Soy desarrollador en el juego de linea B y administrador de esta pagina.
            </span>
        </div>
        <div class="container__interaction">
            <!-- esto depende si es otro perfil el que mira la pagina -->
            <div class="container_follow">
                <button>seguir</button>
            </div>
            <div class="container__change-profile">
                <a href="#">
                    <span>cambiar perfil</span>
                </a>
            </div>
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
<script src="../js/perfil.js" type="text/javascript"></script>
