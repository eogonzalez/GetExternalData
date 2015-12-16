$(document).on("ready", inicio);

function inicio() {
    setInterval(contador(), 360000);
}

function contador()
{
    var contador = document.getElementById("btn_get_metadata_un");
    contador.click();

}