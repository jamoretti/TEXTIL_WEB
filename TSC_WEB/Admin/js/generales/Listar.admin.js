//FUNCION PARA LISTAR Y LLENAR COMBOS
function ListarCombo(metodo, control) {
    $.ajax({
        url: '/dashboard/' + metodo + '/',
        type: 'GET',
        dataType: 'text',
        success: function (e) {
            let input = document.getElementById(control);
            input.innerHTML = e;
        }
    });
}