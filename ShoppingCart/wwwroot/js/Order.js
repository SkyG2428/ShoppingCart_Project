
var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("underprocess")) {
        loadDataTable("underprocess");
    }
    else {
        if (url.includes("shipped")) {
            loadDataTable("shipped");
        }
        else {
            if (url.includes("pending")) {
                loadDataTable("pending");
            }
            else {
                if (url.includes("approved")) {
                    loadDataTable("approved");
                }
                else {
                    loadDataTable("all");
                }
            }
        }
    }
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Order/AllOrders?status=" + status
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "25%" },
            { "data": "phone", "width": "15%" },
            { "data": "address", "width": "15%" },
            { "data": "orderStatus", "width": "15%" },
            { "data": "paymentStatus", "width": "15%" },
            { "data": "orderTotal", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Admin/Order/OrderDetails?id=${data}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>
                      
					</div>
                        `
                },
                "width": "5%"
            }
        ]
    });
}







//var dataTable;

//$(document).ready(function () {
//    var url = window.location.search;
//    if (url.includes("pending")) {
//        loadDataTable("pending");
//    }
//    else {
//        if (url.includes("approved")) {
//            loadDataTable("approved");
//        }
//        else {
//            if (url.includes("shipped")) {
//                loadDataTable("shipped");
//            }
//            else {
//                if (url.includes("underprocess")) {
//                    loadDataTable("underprocess");
//                }
//                else {
//                    loadDataTable("all");
//                }
//            }
//        }
//    }
//});

//function OrderTable(status) {
//    dataTable = $('#myTable').DataTable({
//        "ajax": {
//            "url": "/Admin/Order/AllOrders?status=" + status
//        },
//        "columns": [
            
//            { "data": "name" },
//            { "data": "phone" },
            
//            { "data": "orderStatus" },
//            { "data": "orderTotal" },
//            {
//                "data": "id",
//                "render": function (data) {
//                    return `
//                        <div class="w-75 btn-group" role="group">
//                        <a href="/Admin/Order/OrderDetails?id=${data}"
//                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>
                      
//					</div>
//                        `
//                },

//            }
//        ]
//    });
//}
