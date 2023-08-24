<main class="container">
    <article class="container__perfil">
        <div class="container_info">
            <div class="container__profile-picture">
                <img src="../img/foto_perfil.jpg" alt="">
            </div>
            <div class="container_name">
                <span> <?php echo $rowProfile[0]['nombre_usuario']; ?></span>
            </div>
            <?php if ($rowProfile[0]['verificado'] === "Si") { ?>
                <div class="container_verify" id="container__verify-1">
                    <span class="verify">VERIFICADO</span>
                    <span class="small-verify">V</span>
                    <span class="popup-verify" id="popup-verify-1">Esta persona forma parte de Nevula</span>
                </div>
            <?php } ?>
            <div class="container__change-profile">
                <a href="edit_general.php">
                    <span>EDITAR PERFIL</span>
                </a>
            </div>
        </div>

        <?php if ($rowProfile[0]['descripcion'] === NULL) { ?>
            <div class="container__description">
                <span>
                    Este usuario no ha proporcionado una descripcion.
                </span>
            </div>
        <?php } else { ?>
            <div class="container__description">
                <span>
                   <?php echo $rowProfile[0]['descripcion']; ?>
                </span>
            </div>
        <?php } ?>
        <!-- depende si es otra cuenta se tiene que mostrar seguir o cambiar perfil -->
        <div class="container__interaction">
            <div class="container__posts">
                <span id="posts-id=1"> 1 </span>
                <a href="#" class="posts"> publicaciones </a>
            </div>
            <div class="container__followers">
                <span id="posts-id=1"> 5M </span>
                <a href="#" class="posts"> seguidores </a>
            </div>
            <div class="container__following">
                <span id="posts-id=1"> 6 </span>
                <a href="#" class="posts"> seguidos </a>
            </div>
        </div>
    </article>
    <?php foreach ($rowProfile as $valores){ ?>

    <article class="main__containers-community">

        <div class="container__info">
            <div class="container__profile-pic">
                <a href="profile.php?id=<?php echo $valores['usuario_id'] ?>">
                    <img src="../img/foto_perfil.jpg" alt="">
                </a>
            </div>

            <div class="container__username">
                <a href="profile.php?id=<?php echo $valores['usuario_id'] ?>"><?php echo $valores['nombre_usuario']; ?></a>
            </div>

            <!-- Esto depende si esta o no verificado -->

            <span class="separator__info">·</span>

            <div class="container__date">
                <span><?php echo $valores['fecha']; ?></span>
            </div>
        </div>

        <div class="container__content-post">
            <h2 class="title--post"><?php echo $valores['titulo']; ?></h2>
            <p class="content--post"><?php echo$valores['contenido']; ?></p>
            <div class="container__imgs"></div>
        </div>

        <div class="container__feedback">
            <a href="#" class="feedback__like"><i class="bi bi-heart"></i> 19</a>
            <a href="#" class="feedback__comments"><i class="bi bi-chat"></i> 54</a>
            <a href="#" class="feedback__share"><i class="bi bi-share"></i></a>
        </div>

    </article>
    <?php }?>
</main>


<script src="../js/perfil.js" type="text/javascript"></script>