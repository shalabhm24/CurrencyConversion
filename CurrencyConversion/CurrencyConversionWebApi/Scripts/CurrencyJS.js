$(function () {
    $.ajax({
        url: "/Home/Currency",
        async:true,
        success: function (response) {
            assignToEventsColumns(response);
        },
        error : function (abc){
    }
    });
});

function assignToEventsColumns(data) {
    var table = $('#currencyTable').dataTable({
        "bAutoWidth": false,
        "aaData": data,
        "columns": [{
            "data": "currencyName"
        }, {
            "data": "currencyCode"
        }, {
            "data": "currencyValue"
       }]
    })
}