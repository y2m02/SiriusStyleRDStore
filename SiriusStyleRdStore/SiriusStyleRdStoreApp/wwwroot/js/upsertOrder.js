var grid;
var shippingCost = 0;
var discount = 0;
var subTotal = 0;
var totalPrices = 0;

var checkedValues = [];

$(function () {
    initializeShippingAndDiscount();

    var isFirstLoad = true;
    var isEmptyFilter = false;
    var codes = [];
    grid = window.$("#Products").data("kendoGrid");

    grid.bind("dataBound",
        function () {
            var rows = grid.items();

            if (grid.dataSource.filter() && rows.length == 0) {
                codes = checkedValues;
                isEmptyFilter = true;
                return;
            }
            if (isEmptyFilter) {
                window.$(rows).each(function (e) {
                    var row = this;
                    var dataItem = grid.dataItem(row);

                    if (codes.includes(dataItem.ProductCode)) {
                        grid.select(row);
                    }
                });

                isEmptyFilter = false;
                return;
            }

            if (isFirstLoad) {
                window.$(rows).each(function (e) {
                    var row = this;
                    var dataItem = grid.dataItem(row);

                    if (dataItem.OrderNumber != null) {
                        grid.select(row);
                    }
                });

                isFirstLoad = false;
            }
        });
});

function isValid() {
    var customerIdIsValid = buildError("cbxCustomers", "lblCustomersError");
    var statusIsValid = buildError("cbxStatus", "lblStatusError");
    var productsIsValid = true;

    var total = totalPrices - discount;

    if (total <= 0) {
        window.$("#lblProductsError").html("Debe seleccionar al menos un artículo");
        productsIsValid = false;
    }

    return customerIdIsValid
        && statusIsValid
        && productsIsValid;
}

function buildOrder() {
    var orderNumber = window.$("#txtOrderNumber").val();
    var customerId = window.$("#cbxCustomers").val();
    var status = window.$("#cbxStatus").val();
    var subTotal = window.$("#lblSubTotal").html();
    var total = window.$("#lblTotal").html();
    var productCodes = checkedValues;

    return {
        "OrderNumber": orderNumber,
        "CustomerId": customerId,
        "Status": status,
        "ShippingCost": shippingCost,
        "Discount": discount,
        "SubTotal": subTotal,
        "Total": total,
        "ProductCodes": productCodes
    };
}

function saveOrder() {
    document.body.style.cursor = 'wait';
    if (!isValid()) {
        document.body.style.cursor = 'default';
        return false;
    }
    var order = buildOrder();

    window.$.ajax({
        url: "/Order/AjaxUpsert",
        data: order,
        type: "POST",
        content: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            document.body.style.cursor = 'default';
            window.location = "/";
        },
        error: function (errorMessage) {
            document.body.style.cursor = 'default';
            alert(errorMessage.responseText);
        }
    });

    return true;
}

function onChange(e) {
    window.$("#lblProductsError").html("");
    checkedValues = getCheckedValues(grid);

    var rows = e.sender.select();
    totalPrices = 0;
    rows.each(function () {
        var dataItem = grid.dataItem(this);
        totalPrices += parseFloat(dataItem.Price);
    });

    subTotal = totalPrices + shippingCost;
    var total = totalPrices - discount;
    setTotalAndSubTotal(subTotal, total);
}

function getCheckedValues(grid) {
    var selectedRows = grid.selectedKeyNames();

    var productCodes = [];
    for (var i = 0; i < selectedRows.length; i++) {
        productCodes.push(selectedRows[i]);
    }

    return productCodes;
}

window.$("#txtShippingCost, #txtDiscount").on("input",
    function () {
        initializeShippingAndDiscount();

        setTotalAndSubTotal(totalPrices + shippingCost, totalPrices - discount);
    });


function initializeShippingAndDiscount() {
    shippingCost = window.$("#txtShippingCost").val() == ""
        ? 0.00
        : parseFloat(window.$("#txtShippingCost").val());
    discount = window.$("#txtDiscount").val() == ""
        ? 0.00
        : parseFloat(window.$("#txtDiscount").val());
}

function setTotalAndSubTotal(subTotal, total) {
    window.$("#lblSubTotal").html(subTotal.toFixed(2));
    window.$("#lblTotal").html(total.toFixed(2));
}

$("#btnSaveOrder").on("click",
    function () {
        saveOrder();
    });

$("#btnGoBack").on("click",
    function () {
        window.location = "/";
    });