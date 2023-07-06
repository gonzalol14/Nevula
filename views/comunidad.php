<main class="container">

    <!--<article class="menu__comunnity">
        <a href="../controllers/post_publication.php" class="btn__create-post-text">
            <span>Crear publicación</span>
        </a>
    </article>-->

    <?php foreach($rowPublications as $valores){ ?>

    <article class="main__containers-community">

        <div class="container__info">
            <div class="container__profile-pic">
                <a href="profile.php?id=<?php echo ($valores['usuario_id']) ?>">
                    <img src="../img/foto_perfil.jpg" alt="">
                </a>
            </div>

            <div class="container__username">
                <a href="profile.php?id=<?php echo ($valores['usuario_id']) ?>"><?php echo $valores['nombre_usuario'] ?></a>
            </div>

            <!-- Esto depende si esta o no verificado -->
            <div class="container__verify" id="container__verify-1">
                <span class="verify">VERIFICADO</span>
                <span class="small-verify">V</span>
                <span class="popup-verify" id="popup-verify-1">Esta persona forma parte de Nevula</span>
            </div>

            <span class="separator__info">·</span>

            <div class="container__date">
                <span><?php echo $valores['fecha'] ?></span>
            </div>
        </div>

        <div class="container__content-post">
            <h2 class="title--post"><?php echo $valores['titulo'] ?></h2>
            <p class="content--post"><?php echo $valores['contenido'] ?></p>
            <div class="container__imgs"></div>
        </div>

        <div class="container__feedback">
            <a href="#" class="feedback__like"><i class="bi bi-heart"></i> 19</a>
            <a href="publication.php" class="feedback__comments"><i class="bi bi-chat"></i> 54</a>
            <a href="#" class="feedback__share"><i class="bi bi-share"></i></a>
        </div>

    </article>
    <?php } ?>

</main>


<script src="../js/comunidad.js" type="text/javascript"></script>