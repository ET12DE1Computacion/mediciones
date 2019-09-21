var baseURL = window.location.protocol + "//" + window.location.host + "/"; // + location.pathname.split('/')[1] + "/";
var grdMediciones;
var chartMediciones;
var DELAY = 1000;
var myChart;
var puntosTimeOut, tablaTimeOut;

$(document).ready(function () {
    VerTabla();
});

function VerTabla() {

    //$('#pnlGrafico').removeClass('active');
    $('#pnlTabla').addClass('active');
    $('#contenido').empty();
    clearTimeout(puntosTimeOut);
    clearTimeout(tablaTimeOut);

    $('#contenido').append('<table id="grdMediciones" class="table is-bordered is-hoverable is-striped is-narrow"> <thead> <tr> <th>Nro. Medición</th> <th>Valor</th> <th>Hora</th> <th>Tipo</th> </tr> </thead></table>');

    grdMediciones = $('#grdMediciones').DataTable({
        "aoColumnDefs": [{
            "targets": [],
            "visible": false,
            "sType": "html",
            "aTargets": [1, 2, 3]
        }],
        "aoColumns": [
            {
                "data": "idMedicion"
            },
            {
                "data": "valor",
                "mRender": function (data, type, row) {
                    if (row.tipoMedicion === 'humedad') {
                        return row.valor + ' %';
                    }
                    return row.valor + ' °C';
                }
            },
            {
                "data": "fechaHora",
                "mRender": function (data, type, row) {
                    var value = new Date(row.fechaHora);
                    return value.toLocaleTimeString() + ' Hs';
                }
            },
            {
                "data": "tipoMedicion",
                "mRender": function (data, type, row) {
                    if (row.tipoMedicion === 'sensacionTermica') {
                        return 'Sensacion Térmica';
                    }
                    if (row.tipoMedicion === 'humedad') {
                        return 'Humedad';
                    }
                    if (row.tipoMedicion === 'temperatura') {
                        return 'Temperatura';
                    }
                    return 'Medición desconocida';
                }
            }],
        "order": [0, "desc"],
        "autoWidth": false,
        "sorting": false,
        "searching": false,
        "ordering": true,
        "paging": false,
        "info": false,
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
        url: baseURL + "Medicion/GetUltimaMedicionTemperatura",
        type: 'GET',
        contentType: 'application/json; charset=utf-8'
    });


    request.done(function (data) {
        //console.log(data);
        //grdMediciones.clear();
        grdMediciones.row.add(data);
        grdMediciones.draw();
        tablaTimeOut = setTimeout(CargarTabla, DELAY);
    });

    request.fail(function (data) {
        console.log(data);
    });
}

function CargarTablaST() {
    var request = $.ajax({
        url: baseURL + "Medicion/GetUltimaMedicionSensacionTermica",
        type: 'GET',
        contentType: 'application/json; charset=utf-8'
    });


    request.done(function (data) {
        //grdMediciones.clear();
        grdMediciones.row.add(data);
        grdMediciones.draw();
        tablaTimeOut = setTimeout(CargarTablaST, DELAY);
    });

    request.fail(function (data) {
        console.log(data);
    });
}

function CargarTablaH() {
    var request = $.ajax({
        url: baseURL + "Medicion/GetUltimaMedicionHumedad",
        type: 'GET',
        contentType: 'application/json; charset=utf-8'
    });


    request.done(function (data) {
        //console.log(data.Response);
        //grdMediciones.clear();
        grdMediciones.row.add(data);
        grdMediciones.draw();
        tablaTimeOut = setTimeout(CargarTablaH, DELAY);
    });

    request.fail(function (data) {
        console.log(data);
    });
}
var dps = [];
var dataLength = 50; // number of dataPoints visible at any point
