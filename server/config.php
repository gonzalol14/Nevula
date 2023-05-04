<?php

$conn = mysqli_connect('localhost', 'root', '', 'jotelson');

if (!$conn) {
    die('Error de Conexión' . mysqli_connect_error());
}
mysqli_set_charset($conn, 'utf8');

?>