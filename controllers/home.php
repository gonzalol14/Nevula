<?php
session_start();
require_once("../server/config.php");



$title = "Pagina principal";
$view = "home";
require_once('../views/layout.php');