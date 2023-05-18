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

    <article class="article__post">
        <form action="" method="POST">
            <div class="container__title">
                <a href="javascript:history.back()" class="button_back"><i class="bi bi-arrow-left-short"></i></a>
                <div id="title_page">
                    <h2>Creá una publicación</h2>
                </div>
            </div>

            <div class="container__input-title">
                <h3>Título</h3>
                <textarea class="post_textarea" name="title" id="" placeholder="Introduce un título" maxlength="250"></textarea>
            </div>

            <div class="container__input-content">
                <h3>Contenido</h3>
                <textarea class="post_textarea" name="content" id="" placeholder="Introduce texto" maxlength="1750"></textarea>
            </div>

            <div class="container__inputs-finally">
                <button class="button_reset" type="reset">Descartar</button>
                <button class="button_submit" type="submit">Publicar</button>
            </div>
        </form>

    </article>

</main>

<script src="../js/main.js" type="text/javascript"></script>