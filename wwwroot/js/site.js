// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    $('#table').DataTable({
        "language": {
            "url": '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json' // Opcional: Traducción al español
        },
        "searching": "true",

    });
});