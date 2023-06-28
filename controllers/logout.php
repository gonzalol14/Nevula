<?php
session_start();
session_unset();
session_destroy();

if (isset($_COOKIE['email']) || isset($_COOKIE['password'])) {
  unset($_COOKIE['email']);
  unset($_COOKIE['password']);
  setcookie('email', $_POST['email'], time() - 20 * 86400, '/');
  setcookie('password', sha1($_POST['password']), time() - 20 * 86400, '/');
}

header("Location:home.php ");
