//REGISTRAR
function Registrar() {
   
    ///// VALIDAR SI EXISTE EL ESTILO - Y TRAER EL NOMBRE DEL CLIENTE /////
    let estilos = $("#txtEstiloRegistro").val().toUpperCase().trim();
    
    let datos = {
        "estilo": estilos
    }
    $.ajax({
        url: '/DesarrolloProducto/BuscxEstilo',
        type: 'GET',
        data: datos,
        dataType: 'json',
        success: function (response) {
            //DATATABLE
            console.log(response)
         
            $("#idcliente").html("");
            $("#idcliente").text(response.cliente);
            //cliente = response.cliente;
            //siexiste = response.existe;
            Registrar_2();
        },
        error: function (err) {
            alert('Error:' + err.responseText);
        }
    });
}
function Registrar_2()
{
    //////////
    
    let estilo = $("#txtEstiloRegistro").val().toUpperCase().trim();
    let observacion = $("#txtObservacion").val().trim();
    let archivo = $("#fileFicha")[0].files[0];
    let archivovalida = document.getElementById("fileFicha").files.length;
    let cliente 
    //let cliente = $("#idcliente").text().toUpperCase().trim();



    if (estilo != ""  && archivovalida > 0 && $("#idcliente").text != "") {
        let formData = new FormData();
        formData.append("estilo", estilo);
        formData.append("observacion", observacion);
        formData.append("cliente", $("#idcliente").text().toUpperCase().trim());
        formData.append("archivo", archivo);

        //PROCESO DE CARGA
        let contenido = `<div class="spinner-border spinner-border-sm text-light" role="status"></div> Subiendo...`;
        $("#btnRegistrar").html(contenido);
     

        $.ajax({
            url: '/DesarrolloProducto/RegistrarFichas/',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (e) {
                if (!e.error) {
                    $("#frmArchivos")[0].reset();
                    Informar(e.mensaje);
                    $("#idcliente").text("");
                    //CIERRA MODAL
                    $("#ModalRegistro").modal("hide");
                    //RESETEA SELECT 2
                    $('#cboAreasRegistro').val("1").trigger('change');
                } else {
                    Advertir(e.mensaje);
                    $("#idcliente").text("");
                }
            }
        });


    } else {
        Advertir("Algunos campos estan incompletos");
    }
}

 
