//DESACTIVANDO TODOS LOS MODULOS
function ModulosSubmodulosActivar(modulo, submodulo) {


    $(".Modulos").removeClass("active");
    $("#Modulo_" + modulo).addClass("active");
    $("#Menu_" + modulo).addClass("menu-open");

    $("#SubModulo_" + modulo).attr("style","display:block");
    $("#" + modulo + "-" + submodulo).addClass("active");
}


Date.prototype.getWeekNumber = function () {
    var d = new Date(+this);  //Creamos un nuevo Date con la fecha de "this".
    d.setHours(0, 0, 0, 0);   //Nos aseguramos de limpiar la hora.
    d.setDate(d.getDate() + 4 - (d.getDay() || 7)); // Recorremos los días para asegurarnos de estar "dentro de la semana"
    //Finalmente, calculamos redondeando y ajustando por la naturaleza de los números en JS:
    return Math.ceil((((d - new Date(d.getFullYear(), 0, 1)) / 8.64e7) + 1) / 7);
};