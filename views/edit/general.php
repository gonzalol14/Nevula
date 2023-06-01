<section class="container__section">
    <h2 class="title__edit">Ajustes generales</h2>

    <form action="" method="post">
        <div class="form-group">
            <label for="" class="input-title">Nombre de usuario</label>
            <input type="text" name="username" class="" value="">
        </div>

        <div class="form-group">
            <label for="email" class="input-title">Dirección de correo</label>
            <input type="email" name="email" class="" value="">
        </div>

        <div class="container__full-name">
            <div class="form-group container__name">
                <label for="name" class="input-title">Nombre</label>
                <input type="text" name="name" class="">
                <div class="msj_error">
                </div>
            </div>

            <div class="form-group container__surname">
                <label for="surname" class="input-title">Apellido</label>
                <input type="text" name="surname" class="">
                <div class="msj_error">
                </div>
            </div>
        </div>


        <div class="form-group">
            <label for="" class="input-title">Descripción</label>
            <textarea name="description" id="" maxlength="250"></textarea>
        </div>

        <div class="form-submit">
            <button type="reset" class="btn_edit-reset" id="edit_general-reset">Descartar</button>
            <button type="submit" class="btn_edit-submit" id="edit_general-submit">Guardar cambios</button>
        </div>
    </form>

</section>