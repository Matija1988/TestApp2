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
                { data: 'abrv' },
                { data: 'name' },
                {
                    data: 'id',
                    "render": function (data) {
                        return `<div class="w-75 btn-group" role="group">
                        <a href="/VehicleMaker/Edit/${data}" class="btn btn-primary pr-3 pl-3">Edit</a>
                        <a onClick=Delete("/VehicleMaker/Delete/${data}") class="btn btn-danger mx-2">Delete</a>
                        </div>`

                    },
                    "width": "15%"
                },
            ]
        });
}

function Delete(url) {
    if (confirm("This will delete the entry premanently! Continue?")) {
        $.ajax({
            url: url,
            type: 'DELETE',
            success: function (data) {
                dataTable.ajax.reload();
            }
            
        });
    }
}
