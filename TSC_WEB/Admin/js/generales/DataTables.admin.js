function ArmarDataTable(tabla,exportar = false,minimo = true) {

    /*var tbl =  $(`#${tabla}`).DataTable();
    tbl.destroy();*/

    var objeto = {};
    objeto.language = { 'url': "/Libs/datatables/Spanish.json" };
    objeto.ordering = false;

    if(minimo){
        objeto.lengthMenu = [[5, 10, 20, -1], [5, 10, 20, 'Todos']];
    }

    if(exportar){
        objeto.dom = 'Bfrtip';
        objeto.buttons = ['excel', 'pdf','print'];
    }

    $(`#${tabla}`).DataTable(objeto);

}