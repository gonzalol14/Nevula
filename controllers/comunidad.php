<?php
require_once("../server/config.php");
$sqlPublications = "SELECT publicaciones.*, usuarios.nombre_usuario FROM publicaciones 
                    LEFT JOIN usuarios 
                    ON publicaciones.usuario_id = usuarios.id 
                    WHERE publicaciones.deleted_at IS NULL 
                    ORDER BY publicaciones.fecha DESC";
$resultPublications = mysqli_query($conn, $sqlPublications);
if (!$resultPublications) {
    die('Error de consulta: ' . mysqli_error($conn));
}
$rowPublications = mysqli_fetch_all($resultPublications, MYSQLI_ASSOC);

$title = "Comunidad";
$view = "comunidad";
require_once('../views/layout.php');
