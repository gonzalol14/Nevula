<?php
session_start();
require_once("../server/config.php");
if (!isset($_SESSION['usuario'])) {
    header("Location:login.php");
}
if ($_SESSION['usuario']['id'] != $_GET['id']) {
    header("Location:home.php");
}
if (!empty($_POST)) {
    $cpass = $_POST['currentpass'];
    $npass = $_POST['newpass'];
    $id = $_SESSION['usuario']['id'];

    $sqlVerify = "SELECT clave FROM usuarios WHERE id= '" . $_SESSION['usuario']['id'] . "'";
    $resultVerify = mysqli_query($conn, $sqlVerify);
    $rowVerify = mysqli_fetch_assoc($resultVerify);

    if (mysqli_num_rows($resultVerify) > 0) {
        if ($rowVerify['clave'] == sha1($cpass) && $cpass != null && $npass != null && strlen($npass) > 7) {

            $sqlChangePass = "UPDATE usuarios SET clave = '" . sha1($npass) . "'WHERE id= '" . $_SESSION['usuario']['id'] . "'";
            $resultChangePass = mysqli_query($conn, $sqlChangePass);
            if ($resultChangePass) {
                if (isset($_COOKIE['password'])) {
                    setcookie('password', sha1($npass), time() + 20 * 86400, '/');

                    $_COOKIE['password'] = sha1($npass);
                    header("Location:profile.php?id=$id");
                }
            } else {
                die("Error de consulta: " . mysqli_error($conn));
            }
        } else {
            // Contraseña vieja que puso esta mal
            if (isset($rowVerify['password']) != sha1($cpass)) $message['error_oldpass'] = "La clave actual no coincide";

            if (strlen($_POST['newpass']) < 8) $message['error_newpass'] = "Ingrese una combinacion con al menos ocho caracteres";
            // Campos vacios 
            if (!$cpass) $message['error_oldpass'] = "Por favor ingrese su clave actual";
            if (!$npass) $message['error_newpass'] = "Por favor ingrese una nueva clave";
        }
    }
}
$title = "Cambiar contraseña";
$view = "edit_profile";
require_once('../views/layout.php');
