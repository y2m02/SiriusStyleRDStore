﻿function dataBound() {
    this.expandRow(this.tbody.find("tr.k-master-row").first());

    var grid = window.$("#Orders").data("kendoGrid");
    var totalPending = 0;
    var totalPaidAndShipped = 0;
    var rows = grid.items();

    window.$(rows).each(function(e) {
        var row = this;
        var dataItem = grid.dataItem(row);

        switch (dataItem.Status) {
        case "Pendiente":
            totalPending += parseFloat(dataItem.Total);
            break;
        case "Paga":
        case "Enviada":
            totalPaidAndShipped += parseFloat(dataItem.Total);
            break;
        }
    });

    var grantTotal = totalPending + totalPaidAndShipped;
    window.$("#lblTotalPending").html(totalPending.toFixed(2));
    window.$("#lblTotalPaidAndShipped").html(totalPaidAndShipped.toFixed(2));
    window.$("#lblGrantTotal").html(grantTotal.toFixed(2));
}

$("#createOrder").on("click",
    function() {
        window.location = "/Order/Upsert";
    });

$("#Orders").delegate(".editButton",
    "click",
    function() {
        var grid = window.$("#Orders").data("kendoGrid");
        var rowData = grid.dataItem(window.$(this).closest("tr"));

        window.location = "/Order/Upsert?orderNumber=" + rowData.OrderNumber;

    });

var orderNumber;

function showConfirm(e) {
    e.preventDefault();

    var dataItem = this.dataItem(window.$(e.currentTarget).closest("tr"));
    orderNumber = dataItem.OrderNumber;

    window.$("#myCancellationModal").modal();
}

function cancelOrder() {
    document.body.style.cursor = "wait";

    var order = {
        "OrderNumber": orderNumber
    };

    window.$.ajax({
        url: "/Order/Cancel",
        data: order,
        type: "POST",
        content: "application/json;charset=utf-8",
        dataType: "json",
        success: function(result) {
            document.body.style.cursor = "default";
            location.reload();
        },
        error: function(errorMessage) {
            document.body.style.cursor = "default";
            alert(errorMessage.responseText);
        }
    });
}