$("#Products").delegate(".editButton",
    "click",
    function (e) {
        var grid = window.$("#Products").data("kendoGrid");
        var rowData = grid.dataItem(window.$(this).closest("tr"));

        //GetDropDownListData("cbxCategories", rowData.CategoryId, "Category");
        fillFields(rowData);

        window.$("#myModalProduct").modal();
    });

function fillFields(rowData) {
    var image = rowData.Image;
    if (image != null) {
        var fileName = image.split("_")[1];
        window.$(".custom-file-input").siblings(".custom-file-label").addClass("selected").html(fileName);
    }

    var categoryId = checkIfValueExists("cbxCategories", rowData.CategoryId)
        ? rowData.CategoryId
        : "";

    var sizeId = checkIfValueExists("cbxSizes", rowData.SizeId)
        ? rowData.SizeId
        : "";

    window.$("#cbxCategories").val(categoryId);
    window.$("#txtProductCode").val(rowData.ProductCode);
    window.$("#txtDescription").val(rowData.Description);
    window.$("#txtPrice").val(rowData.Price);
    window.$("#cbxSizes").val(sizeId);
    window.$("#txtComments").val(rowData.Comments);
}

$("#myModalProduct").on("hidden.bs.modal",
    function () {
        window.$(".custom-file-input").siblings(".custom-file-label").addClass("selected").html("Elija una imagen");
        window.$("#cbxCategories").val("");
        window.$("#txtProductCode").val("");
        window.$("#txtDescription").val("");
        window.$("#txtPrice").val("");
        window.$("#cbxSizes").val("");
        window.$("#txtComments").val("");

        clearErrorMessage([
            {
                'key': "txtDescription",
                'value': "lblDescriptionError"
            },
            {
                'key': "txtPrice",
                'value': "lblPriceError"
            },
            {
                'key': "cbxCategories",
                'value': "lblCategoriesError"
            },
        ]);
    });

$(".custom-file-input").on("change",
    function () {
        var fileName = window.$(this).val().split("\\").pop();
        window.$(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });

$(function () {
    window.$("input[id*='txtPrice']").keydown(function (event) {

        if (event.shiftKey == true) {
            event.preventDefault();
        }

        if (!((event.keyCode >= 48 && event.keyCode <= 57) ||
            (event.keyCode >= 96 && event.keyCode <= 105) ||
            event.keyCode == 8 ||
            event.keyCode == 9 ||
            event.keyCode == 37 ||
            event.keyCode == 39 ||
            event.keyCode == 46 ||
            event.keyCode == 190)) {

            event.preventDefault();
        }

        if (window.$(this).val().indexOf(".") !== -1 && event.keyCode == 190)
            event.preventDefault();
        //if a decimal has been added, disable the "."-button
    });
});

function validate() {
    var categoryIdIsValid = buildError("cbxCategories", "lblCategoriesError");
    var descriptionIsValid = buildError("txtDescription", "lblDescriptionError");
    var priceIsValid = buildError("txtPrice", "lblPriceError");

    return categoryIdIsValid
        && descriptionIsValid
        && priceIsValid;
}

//$("#openModal").on("click",
//    function () {
//        GetDropDownListData("cbxCategories", null, "Category");
//    });