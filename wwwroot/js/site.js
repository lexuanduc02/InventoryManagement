let table = new DataTable('#dtgrView', {
    responsive: true,
    processing: true,
    lengthMenu: [15, 25, 50, 100],
});

let history = new DataTable('.dtgrView', {
    responsive: true,
    processing: true,
    lengthMenu: [5, 10],
});

$(document).ready(function () {
    $('.select2').select2();
});