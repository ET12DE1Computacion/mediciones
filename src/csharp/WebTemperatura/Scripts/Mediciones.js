var baseURL = window.location.protocol + "//" + window.location.host + "/";
var grdMediciones;
var chartMediciones;
var DELAY = 1000;
var myChart;
var puntosTimeOut, tablaTimeOut;

$(document).ready(function () {

});

function VerTabla() {
        
    $('#contenido').empty();
    clearTimeout(puntosTimeOut);

    $('#contenido').append('<table id="grdMediciones" class="table table-bordered table-hover table-striped"> <thead> <tr> <th>Nro. Medición</th> <th>Valor</th> <th>Hora</th> <th>Tipo</th> </tr> </thead><tfoot> <tr> <th>Nro. Medición</th> <th>Valor</th> <th>Hora</th> <th>Tipo</th> </tr> </tfoot></table>');

    grdMediciones = $('#grdMediciones').DataTable({
        "aoColumnDefs": [{
            "targets": [],
            "visible": false,
            "sType": "html",
            "aTargets": [2, 3]
        }],
        "aoColumns": [
            {
                "data": "idMedicion"
            },
            {
                "data": "valor",
                "mRender": function (data, type, row) {
                    return row.valor + ' °C';
                }
            },
            {
                "data": "fechaHora",
                "mRender": function (data, type, row) {
                    var value = new Date(parseInt(row.fechaHora.replace(/(^.*\()|([+-].*$)/g, '')));
                    return value.toLocaleTimeString() + ' Hs';
                }
            },
            {
                "data": "idTipoMedicion",
                "mRender": function (data, type, row) {
                    if (row.idTipoMedicion == '2') {
                        return 'Sensacion Termica';
                    }
                    if (row.idTipoMedicion == '3') {
                        return 'Humedad';
                    }
                    return 'Temperatura';
                }
            }],
        "order": [0, "desc"],
        "autoWidth": false,
        "searching": false,
        "ordering": true,
        "paging": false,
        "scrollY": "200px",
        "language": {
            "decimal": "",
            "emptyTable": "No hay información disponible para mostrar",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ mediciones",
            "infoEmpty": "No hay entradas para mostrar",
            "infoFiltered": "(filtrados de _MAX_ entradas totales)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Ver _MENU_ mediciones",
            "loadingRecords": "Cargando...",
            "processing": "En proceso...",
            "search": "Buscar",
            "zeroRecords": "No hay entradas que coincidan con el criterio de busqueda",
            "paginate": {
                "first": "Primera",
                "last": "Última",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });

    CargarTabla();
}

function CargarTabla() {
    var request = $.ajax({
        url: baseURL + "Medicion/GetUltimaMedicionTabla",
        type: 'GET',
        contentType: 'application/json; charset=utf-8'
    });


    request.done(function (data) {
        //console.log(data.Response);
        //grdMediciones.clear();
        grdMediciones.row.add(data.Response);
        grdMediciones.draw();
        tablaTimeOut = setTimeout(CargarTabla, DELAY);
    });

    request.fail(function (data) {
        console.log(data.Response);
    });
}

var dps = [];
var dataLength = 50; // number of dataPoints visible at any point


function VerGrafico() {

    $('#contenido').empty();
    clearTimeout(tablaTimeOut);

    $('#contenido').attr('style', 'height: 400px; max-width: 1100px; margin: 0px auto;');

    $('#contenido').append('<canvas id="myChart" width="1000" height="300"></canvas>');

    var ctx = document.getElementById("myChart").getContext('2d');

    myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
            datasets: [{
                label: 'Temperatura',
                data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 2
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    },
                    display: true,
                    labelString: 'dasdas'
                }]
            }
        }
    });

    cargarPuntos();
}

function cargarPuntos() {

    var baseURL = window.location.protocol + "//" + window.location.host + "/";
    //console.log(baseURL);
    var req = $.ajax({
        url: baseURL + 'Medicion/GetUltimaMedicion',
        type: 'GET',
        contentType: 'application/json; context=utf-8'
    });

    req.done(function (data) {
        //console.log(data.Response);
        myChart.data.labels.shift();
        myChart.data.datasets[0].data.shift();
        var value = new Date(parseInt(data.Response.fechaHora.replace(/(^.*\()|([+-].*$)/g, '')));
        myChart.data.labels.push(value.toLocaleTimeString() + ' Hs');
        myChart.data.datasets[0].data.push(data.Response.valor);
        myChart.update();
        puntosTimeOut = setTimeout(cargarPuntos, 1000);
    });
}