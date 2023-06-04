<main class="container container__edit">
    <aside class="aside__menu-edit main__containers-community">
        <ul class="submenu__edit-profile ">
            <li>
                <a href="edit_general.php" class="<?php echo ($title == "Ajustes generales") ? "active" : null; ?>">GENERAL</a>
            </li>
            <li>
                <a href="edit_avatar.php" class="<?php echo ($title == "Cambiar avatar") ? "active" : null; ?>">AVATAR</a>
            </li>
            <li>
                <a href="edit_password.php" class="<?php echo ($title == "Cambiar contraseña") ? "active" : null; ?>">CONTRASEÑA</a>
            </li>
        </ul>
    </aside>
    <article class="article__edit main__containers-community">
        <?php
        if ($title == "Ajustes generales") {
            require_once("../views/edit/general.php");
        } else if ($title == "Cambiar contraseña") {
            require_once("../views/edit/password.php");
        } else {
            require_once("../views/edit/avatar.php");
        }
        ?>
    </article>
</main>


