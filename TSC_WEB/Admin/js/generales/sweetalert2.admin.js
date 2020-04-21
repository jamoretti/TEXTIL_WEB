// ##############
// ## MENSAJES ##
// ##############

// ADVERTIR
function Advertir(mensaje) {
    swal.fire(mensaje, "Textile Web", "error");
}
//INFORMAR -- SUCCESS
function Informar(mensaje) {
    swal.fire(mensaje,"Textile Web","success");
}

//PREGUNTAR
async function Preguntar(pregunta){
    let resultado = await Swal.fire({
        title: pregunta,
        text: "Textile Web",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirmar'
    });

    return resultado;
}

function MostrarCarga(mensaje){

    Swal.fire({
        title: mensaje,
        html: 'Esto puede tardar unos minutos',
        //timer: 2000,
        allowEscapeKey: false,
        allowOutsideClick: false,
        timerProgressBar: true,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    })    
}

//LOGIN ACCESO
function EntrarSistema(mensaje) {
    let timerInterval;

    Swal.fire({
        title: mensaje,
        icon: 'success',
        html: 'Cargando...',
        timer: 1000,
        onBeforeOpen: () => {
            Swal.showLoading()
        },
        onClose: () => {
            clearInterval(timerInterval)
        }
    }).then((result) => {
        if (
            /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.timer
        ) {
            //REDIRECCIONANDO 
            location.href = "/dashboard/index";
        }
    })
}

