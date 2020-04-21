//REGISTRAR
function Registrar() {
   
    ///// VALIDAR SI EXISTE LA PO /////
    let po = $("#txtPORegistro").val().toUpperCase().trim();
    
    let datos = {
        "po": po
    }
    $.ajax({
        url: '/Comercial/BuscxPO',
        type: 'GET',
        data: datos,
        dataType: 'json',
        success: function (response) {
            //DATATABLE    
            $("#idcliente").html("");
            $("#idcliente").text(response.cliente);
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
    
    let po = $("#txtPORegistro").val().toUpperCase().trim();
    let observacion = $("#txtObservacion").val().trim();
    let archivo = $("#fileFicha")[0].files[0];
    let archivovalida = document.getElementById("fileFicha").files.length;
 
    let cliente = $("#idcliente").text().toUpperCase().trim();



    if (po != ""  && archivovalida > 0 ) 
    {
        if (cliente != "" &&  cliente != null)
        {
            let formData = new FormData();
            formData.append("po", po);
            formData.append("observacion", observacion);
            formData.append("cliente", $("#idcliente").text().toUpperCase().trim());
            formData.append("archivo", archivo);

            //PROCESO DE CARGA
            let contenido = `<div class="spinner-border spinner-border-sm text-light" role="status"></div> Subiendo...`;
            $("#btnRegistrar").html(contenido);
     

            $.ajax({
                url: '/Comercial/RegistrarPOS/',
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
                    } else {
                        Advertir(e.mensaje);
                        $("#idcliente").text("");
                    }
                }
            });

        }
        else 
        {
            Advertir("PO no Existe");
        }
    } 
    else 
    {
        Advertir("Algunos campos estan incompletos");
    }
}

 
