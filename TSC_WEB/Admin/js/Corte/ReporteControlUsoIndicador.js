//ARMA GRAFICO INDICADOR
function ArmarGrafico1(objeto, arraytizado, arraycortado, arraydescripcion, array4) {


    var ctx = document.getElementById(objeto).getContext('2d');

    var dataporcentaje2 = [];
    for (var i = 0 ; i < array4.length; i++) {
        dataporcentaje2.push(array4[i] * 500 / 100);
    }



    var mixedChart = new Chart(ctx, {
        type: 'bar',
        data: {
            datasets: [
            {
                label: 'Tizado',
                data: arraytizado,
                order: 1,
                backgroundColor: '#9ad0f5',
                borderColor: '#67b8f0',
            },
            {
                label: 'Cortado',
                data: arraycortado,
                order: 1,
                backgroundColor: '#ffb0c1',
                borderColor: '#ff7592',
            },
            {
                label: '% Registro',
                data: dataporcentaje2,
                type: 'line',
                fill: false
            }],
            labels: arraydescripcion
        },
        options: {
            responsive: true,
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Indicador de utilización'
            },
            scales: {
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: 500,
                        stepSize: 100
                    }
                }]
            },
		tooltips: {
                        enabled: false
                    },
                    animation: {
                        onComplete: function () {
                            var ctx = this.chart.ctx;
                            //console.log(ctx);
                            ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontFamily, 'normal', Chart.defaults.global.defaultFontFamily);
                            ctx.fillStyle = "black";
                            ctx.textAlign = 'center';
                            ctx.textBaseline = 'bottom';

                            this.data.datasets.forEach(function (dataset)
                            {
                                
                                    for (var i = 0; i < dataset.data.length; i++) {
                                        for(var key in dataset._meta)
                                        {
                                            var model = dataset._meta[key].data[i]._model;
                                            var valor = 0;
                                            if(dataset.type=="line"){
                                                 valor = (dataset.data[i] * 100 /500) + "%";
                                            }else{
                                                valor = dataset.data[i];
                                            }

                                            ctx.fillText(valor, model.x, model.y - 5);
                                        }
                                    }
                                
                                
                            });
                        }
                    }
        }
    });




}