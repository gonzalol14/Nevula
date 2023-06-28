<?php
require_once("../server/config.php");
session_start();

if (!isset($_SESSION['usuario'])) {
    header("Location:login.php");
}
if (!empty($_POST)) {
    $title = $_POST['title'];
    $content = $_POST['content'];

    if (($title != null && strlen($title) <= 250) &&  ($content != null && strlen($content) <= 1750)) {

        $sqlPost = "INSERT INTO publicaciones (usuario_id, fecha, titulo, contenido) VALUES ('" . $_SESSION['usuario']['id'] . "',   now() , '" . $title . "', '" . $content . "')";
        $resultPost = mysqli_query($conn, $sqlPost);

        if (!$resultPost) {
            die('Error de consulta: ' . mysqli_error($conn));
        }
        header("Location: comunidad.php");
    } else {

        // maximo de caracteres
        if (strlen($title) > 250) $message['title'] = "El titulo es demasiado largo";
        if (strlen($content) > 1750) $message['content'] = "El contenido es demasiado largo";
        // campos vacios
        if (!$title) $message['title'] = "Ingrese un titulo";
        if (!$content) $message['content'] = "Ingrese el contenido de la publicacion";
    }
}

$title = "Publicar";
$view = "post_publication";
require_once('../views/layout.php');
