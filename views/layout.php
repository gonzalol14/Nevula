<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- JQUERY -->
    <script src="../js/jquery.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" href="../css/main.css">
    <?php
    if ($view == "login" || $view == "register") { ?>
        <link rel="stylesheet" href="../css/login_register.css">
    <?php
    } elseif ($view == "home") { ?>
        <link rel="stylesheet" href="../css/home.css">
    <?php
    } elseif ($view == "comunidad") { ?>
        <link rel="stylesheet" href="../css/comunidad.css">
    <?php
    }?>
    <title><?php echo $title ?> - Jotelson</title>
</head>

<body>

    <?php
    require_once($view . ".php");
    ?>
    
</body>

</html>