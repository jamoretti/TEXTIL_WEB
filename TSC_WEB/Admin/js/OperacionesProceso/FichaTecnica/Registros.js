//REGISTRAR
function RegistrarFicha(idmotivoactualizacion) {
    let idarea = $("#cboAreasRegistro").val();
    let estilo = $("#txtEstiloRegistro").val().toUpperCase().trim();
    let proyecto = $("#txtPedidoRegistro").val().trim();
    let observacion = $("#txtObservacion").val().trim();
    let cliente =    $("#cboCliente option:selected").text()
    let programa =     $("#cboPrograma option:selected").text()
    let archivo = $("#fileFicha")[0].files[0];
    let archivovalida = document.getElementById("fileFicha").files.length;

    if (estilo != "" && proyecto != "" && archivovalida > 0) {
        let formData = new FormData();
        formData.append("idarea", idarea);
        formData.append("estilo", estilo);
        formData.append("proyecto", proyecto);
        formData.append("observacion", observacion);
        formData.append("cliente", cliente);
        formData.append("programa", programa);
        formData.append("idmotivoactualizacion", idmotivoactualizacion);

        formData.append("archivo", archivo);

        //PROCESO DE CARGA
        let contenido = `<div class="spinner-border spinner-border-sm text-light" role="status"></div> Subiendo...`;
        $("#btnRegistrar").html(contenido);
        MostrarCarga("Subiendo Ficha...");

        $.ajax({
            url: '/operaciones/RegistrarFichas/',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (e) {
               
                console.log(e);

                if (!e.error) {
                    $("#frmArchivos")[0].reset();
                    Informar(e.mensaje);
                    //CIERRA MODAL
                    $("#ModalRegistro").modal("hide");
                    //RESETEA SELECT 2
                    $('#cboAreasRegistro').val("1").trigger('change');
                    //RESETEA CLIENTE
                    $("#cboCliente").val('0').trigger('change');

                } else {
                    Advertir(e.mensaje);
                }
            }
        });
    } else {
        Advertir("Algunos campos estan incompletos");
    }

}
