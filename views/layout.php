<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- JQUERY -->
    <script src="../js/jquery.min.js"></script>
    <script src="../js/descarga.js"></script>
    <!-- CSS -->
    <?php
    if ($view == "login" || $view == "register") { ?>
        <link rel="stylesheet" href="../css/login_register.css">
    <?php
    } elseif ($view == "home") { ?>
        <link rel="stylesheet" href="../css/home.css">
    <?php
    } elseif ($view == "comunidad" || $view == "post_publication") { ?>
        <link rel="stylesheet" href="../css/comunidad.css">
        <link rel="stylesheet" href="../css/post_publication.css">
    <?php
    } elseif ($view == "perfil") { ?>
        <link rel="stylesheet" href="../css/comunidad.css">
        <link rel="stylesheet" href="../css/perfil.css">
    <?php
    } ?>
    <link rel="stylesheet" href="../css/main.css">

    <title><?php echo $title ?> - Jotelson</title>
</head>

<body>

    <?php
    require_once("header.php");
    require_once($view . ".php");
    ?>

</body>

</html>