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
    $username = trim($_POST['username']);
    $email = trim($_POST['email']);
    $name = trim($_POST['name']);
    $surname = trim($_POST['surname']);
    $description = trim($_POST['description']);

    $sqlCheckUsername = "SELECT * FROM usuarios WHERE nombre_usuario='$username' AND nombre_usuario != '" . $_SESSION['usuario']['nombre_usuario'] . "'";
    $resultCheckUsername = mysqli_query($conn, $sqlCheckUsername);
    $sqlCheckEmail = "SELECT * FROM usuarios WHERE email='$email' AND email != '" . $_SESSION['usuario']['email'] . "'";
    $resultCheckEmail = mysqli_query($conn, $sqlCheckEmail);

    if ((!mysqli_num_rows($resultCheckUsername) > 0) && (!mysqli_num_rows($resultCheckEmail) > 0) && ($username != null && strlen($username) < 31) && ($name != null && strlen($name) < 21) && ($surname != null && strlen($surname) < 31) && ($email != null && strlen($email) < 101) && (preg_match('/^([a-zA-Z0-9\.]+@+[a-zA-Z]+(\.)+[a-zA-Z]{2,3})$/', $email)) && (strlen($description) < 251)) {
        $sqlEdit = "UPDATE usuarios SET nombre_usuario = '$username', nombre = '$name', email = '$email', descripcion = '$description', apellido = '$surname' WHERE id= '" . $_SESSION['usuario']['id'] . "'";
        $resultEdit = mysqli_query($conn, $sqlEdit);
        if ($resultEdit) {
            $id = $_SESSION['usuario']['id'];

            $sqlNewUser = "SELECT * FROM usuarios WHERE id='" . $id . "' AND deleted_at IS NULL";
            $resultNewUser = mysqli_query($conn, $sqlNewUser);
            if (!$resultNewUser) {
                die("Error de consulta: " . mysqli_error($conn));
            }
            //renuevo session
            if (mysqli_num_rows($resultNewUser) === 1) {

                $rowNewUser = mysqli_fetch_assoc($resultNewUser);

                $_SESSION['usuario'] = $rowNewUser;
            }

            header("Location:profile.php?id=$id");
        } else {
            die("Error de consulta: " . mysqli_error($conn));
        }
    } else {
        // Ya existe una cuenta con ese email
        if (mysqli_num_rows($resultCheckEmail)) $message['error_email'] = "El correo electronico ya esta en uso.";
        // Ya existe una cuenta con ese username 
        if (mysqli_num_rows($resultCheckUsername) > 0) $message['error_username'] =  "El nombre de usuario ya esta en uso";

        // Maximo de caracteres
        if (strlen($username) > 30) $message['error_username'] = "El nombre de usuario es demasiado largo";
        if (strlen($name) > 20) $message['error_name'] = "El nombre es demasiado largo";
        if (strlen($surname) > 30) $message['error_surname'] = "El apellido es demasiado largo";
        if (strlen($email) > 100) $message['error_email'] = "El correo electronico es demasiado largo";
        if (strlen($description) > 250) $message['error_description'] = "La descripcion demasiado larga";

        // Email invalido
        if (!preg_match('/^([a-zA-Z0-9\.]+@+[a-zA-Z]+(\.)+[a-zA-Z]{2,3})$/', $email)) $message['error_email'] = "Ingrese un correo electronico valido";

        // Campos vacios
        if (!$email) $message['error_email'] = "Por favor ingrese un correo electronico";
        if (!$username) $message['error_username'] =  "Por favor ingrese un nombre de usuario";
        if (!$name) $message['error_name'] =  "Por favor ingrese un nombre";
    }
} /*else {
    // No ingreso ningun valor
    $message['reason'] = "Problema en las validaciones";
    $message['error_email'] = "Por favor ingrese un correo electronico";
    $message['error_username'] = "Por favor ingrese un nombre de usuario";
    $message['error_name'] = "Por favor ingrese un nombre";
}*/

$title = "Ajustes generales";
$view = "edit_profile";
require_once('../views/layout.php');
