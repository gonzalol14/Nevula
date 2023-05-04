<?php
require_once("../server/config.php");

if (!empty($_POST)) {
    $name = trim($_POST['name']);
    $surname = trim($_POST['surname']);
    $username = trim($_POST['username']);
    $email = trim($_POST['email']);
    $password = sha1($_POST['password']);
   // $checkbox = '';

    /*  No sé como hacer que funcione
    if(isset($_POST['remember']) && $_POST['remember'] === 1){
        $checkbox = $_POST['remember'];
    }else{
        $message['remember'] = "Debe ser mayor de 13 años y aceptar nuestros terminos y condiciones";
       
    }
   */
    if (isset($_POST['remember']) && $_POST['remember'] === 1) {
        $checkbox = $_POST['remember'];
    }
    // comprobacion de que no exista ninguna cuenta con ese correo
    $sqlCheckEmail = "SELECT * FROM usuarios WHERE email='$email'";
    $resultCheckEmail = mysqli_query($conn, $sqlCheckEmail);

    // comprobacion de que no exista ninguna cuenta con ese nombre de usuario
    $sqlCheckUsername = "SELECT * FROM usuarios WHERE nombre_usuario='$username'";
    $resultCheckUsername = mysqli_query($conn, $sqlCheckUsername);

    if (($name != null && strlen($name) <= 20) && ($surname != null && strlen($surname) <= 30) && ($username != null && strlen($username) <= 30) && ($email != null && strlen($email) <= 100) && ($password != null && strlen($password) > 7) && (!mysqli_num_rows($resultCheckEmail) > 0) && (!mysqli_num_rows($resultCheckUsername) > 0) &&  (preg_match('/^([a-zA-Z0-9\.]+@+[a-zA-Z]+(\.)+[a-zA-Z]{2,3})$/', $email))) {
        // bien
        $sqlRegister = "INSERT INTO usuarios (nombre, apellido, nombre_usuario, email, clave, created_at) VALUES ('" . $name . "', '" . $surname . "', '" . $username . "', '" . $email . "', '" . $password . "', now())";
        $resultRegister = mysqli_query($conn, $sqlRegister);

        if ($resultRegister) {
            $sqlLogin = "SELECT * FROM usuarios WHERE email='" . $email . "' AND clave='" . $password . "' AND deleted_at IS NULL";
            $resultLogin = mysqli_query($conn, $sqlLogin);
        }
        if (!$resultLogin) {
            exit("Error de consulta" . mysqli_error($conn));
        }
        $rowLogin = mysqli_fetch_assoc($resultLogin);
        $_SESSION['usuario'] = $rowLogin;
    } else {

        // Ya existe una cuenta con ese correo
        if (mysqli_num_rows($resultCheckEmail)) $message['email'] =  "La direccion de correo ya esta en uso.";
        // Ya existe una cuenta con ese nombre de usuario 
        if (mysqli_num_rows($resultCheckUsername) > 0) $message['username'] = "El nombre de usuario ya esta en uso";

        // Contraseña con menos de 8 caracteres
        if (strlen($password) < 8) $message['password'] = "Ingrese una combinacion con al menos ocho caracteres";

        // Maximo de caracteres
        if (strlen($name) > 20) $message['name'] = "El nombre es demasiado largo";
        if (strlen($surname) > 30) $message['surname'] = "El apellido es demasiado largo";
        if (strlen($username) > 30) $message['username'] = "El nombre de usuario es demasiado largo";
        if (strlen($email) > 100) $message['email'] = "El correo electronico es demasiado largo";

        // Correo electronico invalido
        if (!preg_match('/^([a-zA-Z0-9\.]+@+[a-zA-Z]+(\.)+[a-zA-Z]{2,3})$/', $email)) $message['email'] = "Ingrese una direccion de correo valida";

        // Campos vacios
        if (!$name) $message['name'] = "Ingrese un nombre";
        if (!$surname) $message['surname'] = "Ingrese un apellido";
        if (!$username) $message['username'] = "Ingrese un nombre de usuario";
        if (!$email) $message['email'] = "Ingrese una direccion de correo";
        if (!$password) $message['password'] = "Ingrese una clave";
    //    if ($checkbox === null) $message['remember'] = "Debe ser mayor de 13 años y aceptar nuestros terminos y condiciones";
    }
}
$view = "register";
require_once('../views/layout.php');
