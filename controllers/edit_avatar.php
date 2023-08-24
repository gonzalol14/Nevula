<?php
session_start();
require_once("../server/config.php");
if ($_SESSION['usuario']['id'] != $_GET['id']){
    header("Location:home.php");
}


$title = "Cambiar avatar";
$view = "edit_profile";
require_once('../views/layout.php');
