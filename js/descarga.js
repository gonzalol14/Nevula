function descargarArchivo() {
    var descarga = document.createElement('a');
    descarga.href = '../download/ejemplo.txt';
    descarga.download = 'ejemplo.txt';
    descarga.click();
}