<?php
require_once("../server/config.php");
/*if (isset($_SESSION['usuario'])) {
    // Todavia no hay pagina principal 
    header("Location:homepage.php");
}*/
if (!empty($_POST)) {
    $email = trim($_POST['email']);
    $password = $_POST['password'];

    $sqlLogin = "SELECT * FROM usuarios WHERE email='" . $email . "' AND clave = '" . sha1($password) . "' AND deleted_at IS NULL";
    $resultLogin = mysqli_query($conn, $sqlLogin);
    
    if (!$resultLogin) {
        die("Error de consulta: " . mysqli_error($conn));
    }
    if (mysqli_num_rows($resultLogin) === 1) {
       /* No funciona
       $rowLogin = mysqli_fetch_assoc($resultLogin);
        session_start();
        
        $_SESSION['usuario'] = $rowLogin; */
        if (isset($_POST['remember']) && $_POST['remember']) {
            
            setcookie('email', $email);
            setcookie('password', $password);
           // setcookie("remember", $remember);
            $_COOKIE['email'] = $email;
            $_COOKIE['password'] = $password;
           // $_COOKIE['remember'] = $remember;
          }else if(!isset($_POST['remember'])){
            unset($_COOKIE['email']);
            unset($_COOKIE['password']);
           // unset($_COOKIE['remember']);
            setcookie('email', null);
            setcookie('password', null);
          //  setcookie('remember', null);
    
            
          }
        // Todavia no hay pagina principal 
        header('Location:home.php');
       
    }else{
        $error = "La direccion de correo ingresada o la contraseña son incorrectas";
    }
        
    
}
$view = "login";
require_once('../views/layout.php');
