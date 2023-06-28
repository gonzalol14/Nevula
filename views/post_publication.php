<main class="container">

    <article class="main__containers-community">
        <form action="../controllers/post_publication.php" method="POST">
            <div class="container__title">
                <a href="javascript:history.back()" class="button_back"><i class="bi bi-arrow-left-short"></i></a>
                <div id="title_page">
                    <h2>Creá una publicación</h2>
                </div>
            </div>

            <div class="container__input-title">
                <h3>Título</h3>
                <textarea class="post_textarea" name="title" id="" placeholder="Introduce un título" maxlength="250"><?php
                                if (isset($_POST["title"])) {
                                    echo '' . $_POST["title"] . '';
                                }
                                ?></textarea>
                <div class="msj_error">
                    <?php echo (isset($message['title']) ? $message['title'] : ''); ?>
                </div>
            </div>

            <div class="container__input-content">
                <h3>Contenido</h3>
                <textarea class="post_textarea" name="content" id="" placeholder="Introduce texto" maxlength="1750"><?php
                                if (isset($_POST["content"])) {
                                    echo '' . $_POST["content"] . '';
                                }
                                ?></textarea>
                <div class="msj_error">
                    <?php echo (isset($message['content']) ? $message['content'] : ''); ?>
                </div>
            </div>

            <div class="container__inputs-finally">
                <button class="button_reset" type="reset">Descartar</button>
                <button class="button_submit" type="submit">Publicar</button>
            </div>
        </form>

    </article>

</main>