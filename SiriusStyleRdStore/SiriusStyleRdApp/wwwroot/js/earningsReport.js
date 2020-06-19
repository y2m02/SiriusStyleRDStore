function dataBound() {
    this.expandRow(this.tbody.find("tr.k-master-row").first());

    var grid = window.$("#EarningReport").data("kendoGrid");
    var totalPending = 0;
    var totalEarned = 0;
    var rows = grid.items();

    window.$(rows).each(function () {
        var row = this;
        var dataItem = grid.dataItem(row);

        totalEarned += parseFloat(dataItem.TotalEarned);
        totalPending += parseFloat(dataItem.TotalPending);
    });

    window.$("#lblTotalEarned").html(convertToNumericFormat(totalEarned));
    window.$("#lblTotalPending").html(convertToNumericFormat(totalPending));
}