<?php
session_start();
require_once("../server/config.php");

if(isset($_GET['id'])){
    if(isset($_SESSION['usuario'])){
        $sqlProfile = "SELECT usuarios.*,publicaciones.*
        FROM usuarios
        LEFT JOIN publicaciones ON usuarios.id = publicaciones.usuario_id
        WHERE usuarios.id = '".$_GET['id']."'  and usuarios.deleted_at IS NULL and publicaciones.deleted_at IS NULL   ";
        
       // $sqlProfile = "SELECT * FROM usuarios WHERE id='".$_GET['id']."' and deleted_at IS NULL";
        $resultProfile = mysqli_query($conn, $sqlProfile);
        if(!$resultProfile){
            die('Error de consulta: ' . mysqli_error($conn));
        }
        $rowProfile = mysqli_fetch_all($resultProfile, MYSQLI_ASSOC);

     //   print_r($rowProfile);
}else{
 header("Location:login.php");
}
}else{
    header('Location:home.php');
}






$title = "Perfil";
$view = "profile";
require_once('../views/layout.php');