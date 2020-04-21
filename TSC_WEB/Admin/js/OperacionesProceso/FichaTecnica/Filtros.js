//LISTAR AREAS
function ListarAreas(control) {
	$.ajax({
		url: '/operaciones/ListarAreas/',
		type: 'GET',
		contenType: 'json',
		success: function (e) {
			//console.log(e);
		    let op = "";
		    if(control == "cboAreasFiltro"){
		        op = "<option value='0'>TODOS LOS REGISTROS</option>";
		    }
			e.forEach((obj)=>{
				op += `<option value='${obj.idarea}'>${obj.nombre_area}</option>`;
			});
			//console.log(op);
		$("#"+control).html(op);
		}
	});
}

//CLIENTES
function ListarClientes(control){
    $.ajax({
        url: '/DesarrolloProducto/ListarClientesFtec/',
        type:'GET',
        datatype: 'json',
        success:function(e){
            //console.log(e);
            let option = "";
            e.forEach(function(obj){
                option += `<option data-cli9="${obj.cliente9}"  data-cli4="${obj.cliente4}" data-cli2="${obj.cliente2}">${obj.nombre_cliente}</option>`;
            });

            $("#cboCliente").html(option);
        }
    });
}