<?php

if (isset($_SESSION['usuario'])) {
    echo "Estas logeado";
} else {
    echo "No estas logeado";
}
?>