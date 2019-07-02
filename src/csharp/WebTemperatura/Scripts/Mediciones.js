var baseURL = window.location.protocol + "//" + window.location.host + "/"; // + location.pathname.split('/')[1] + "/";
var grdMediciones;
var chartMediciones;
var DELAY = 1000;
var myChart;
var puntosTimeOut, tablaTimeOut;

$(document).ready(function () {
    VerGrafico();
    //console.log(baseURL);
});

function VerTabla() {

    $('#pnlGrafico').removeClass('active');
    $('#pnlTabla').addClass('active');
    $('#contenido').empty();
    clearTimeout(puntosTimeOut);
    clearTimeout(tablaTimeOut);

    $('#contenido').append('<table id="grdMediciones" class="table table-bordered table-hover table-striped table-sm"> <thead> <tr> <th>Nro. Medición</th> <th>Valor</th> <th>Hora</th> <th>Tipo</th> </tr> </thead></table>');

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
                    if (row.idTipoMedicion == 3) {
                        return row.valor + ' %';
                    }
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
                    //console.log(row);
                    if (row.idTipoMedicion == 2 || row.idTipoMedicion == 4) {
                        return 'Sensacion Térmica';
                    }
                    if (row.idTipoMedicion == 3) {
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
        "scrollY": "300px",
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
    CargarTablaST();
    CargarTablaH();
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

function CargarTablaST() {
    var request = $.ajax({
        url: baseURL + "Medicion/GetUltimaMedicionTablaST",
        type: 'GET',
        contentType: 'application/json; charset=utf-8'
    });


    request.done(function (data) {
        console.log(data.Response);
        //grdMediciones.clear();
        grdMediciones.row.add(data.Response);
        grdMediciones.draw();
        tablaTimeOut = setTimeout(CargarTablaST, DELAY);
    });

    request.fail(function (data) {
        console.log(data.Response);
    });
}

function CargarTablaH() {
    var request = $.ajax({
        url: baseURL + "Medicion/GetUltimaMedicionTablaH",
        type: 'GET',
        contentType: 'application/json; charset=utf-8'
    });


    request.done(function (data) {
        //console.log(data.Response);
        //grdMediciones.clear();
        grdMediciones.row.add(data.Response);
        grdMediciones.draw();
        tablaTimeOut = setTimeout(CargarTablaH, DELAY);
    });

    request.fail(function (data) {
        console.log(data.Response);
    });
}
var dps = [];
var dataLength = 50; // number of dataPoints visible at any point



function VerGrafico() {

    $('#pnlGrafico').addClass('active');
    $('#pnlTabla').removeClass('active');

    $('#contenido').empty();
    clearTimeout(puntosTimeOut);
    clearTimeout(tablaTimeOut);

    $('#contenido').attr('style', 'height: 400px; max-width: 1100px; margin: 0px auto;');

    $('#contenido').append('<canvas id="myChart" width="1000" height="300"></canvas>');

    var ctx = document.getElementById("myChart").getContext('2d');

    myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
            datasets: [{
                label: 'Mediciones',
                data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                backgroundColor: [
                    'rgba(192,170,189,0.2)'                    
                ],
                borderColor: [
                    'rgba(192,170,189,1)'                    
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
                    scaleLabel: {
                        //fontColor: "white",
                        display: true,
                        labelString: 'Humedad (%)'
                    }
                }],
                xAxes: [{
                    ticks: {
                        beginAtZero: true
                    },
                    scaleLabel: {
                        //fontColor: "white",
                        display: true,
                        labelString: 'Tiempo (hs)'
                    }
                }]
            }
        }
    });

    cargarPuntos();
}

function cargarPuntos() {

    //var baseURL = window.location.protocol + "//" + window.location.host + "/";
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