$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable =
        $('#tblData').DataTable({
            "ajax": {
                url: '/VehicleMaker/getAll',
                type: 'GET',
            },
            "columns": [ 
                { data: 'abrv' },
                { data: 'name' },
                {
                    data: 'id',
                    "render": function (data) {
                        return `<div class="w-75 btn-group" role="group">
                        <a href="/VehicleMaker/Edit/${data}" class="btn btn-secondary pr-3 pl-3">Edit</a>
                        <a onClick=Delete("/VehicleMaker/Delete/${data}") class="btn btn-danger mx-2">Delete</a>
                        </div>`

                    },
                    "width": "15%"
                },

            ], error: function (xhr, status, error) {
                if (xhr.status === 404) {
                    alert(xhr.status + " Data not found");
                }
            }
        
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


