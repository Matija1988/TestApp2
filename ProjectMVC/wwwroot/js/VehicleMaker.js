$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable =
        $('#tblData').DataTable({
            "ajax": {
                url: '/VehicleMaker/getAll'
            },
            "columns": [
                { data: 'id', "width":"10%" }, 
                { data: 'abrv' },
                { data: 'name' },
            ]
        });
}
