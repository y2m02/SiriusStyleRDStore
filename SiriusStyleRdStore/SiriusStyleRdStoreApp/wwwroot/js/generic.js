function clearErrorMessage(parameters) {
    for (var i = 0; i < parameters.length; i++) {
        window.$("#" + parameters[i].key).css("borderColor", "");
        window.$("#" + parameters[i].value).html("");
    }
}

function removeErrorMessage(id, messageId) {
    window.$("#" + id).css("borderColor", "");
    window.$("#" + messageId).html("");
}

function buildError(field, label) {
    var fieldId = "#" + field;

    if (window.$(fieldId).val() === "") {
        window.$(fieldId).css("border-color", "Red");
        window.$("#" + label).html("Campo requerido");
        return false;
    }

    window.$(fieldId).css("border-color", "");

    return true;
}

function onClick(img) {
    var src = img.currentSrc;

    window.$("#imageId").attr("src", src);
    window.$("#myModalImage").modal();
}

$(function() {
    window.$("input").attr("autocomplete", "off");
});


function GetDropDownListData(elementId, id, controllerName) {
    window.$.ajax({
        url: "/" + controllerName + "/GetAllForDropDownList",
        data: { id: id },
        type: "GET",
        content: "application/json;charset=utf-8",
        dataType: "json",
        success: function(result) {
            fillDropDownList(elementId, result);
            
            if (id != null) {
                window.$("#" + elementId).val(id);
            }
        },
        error: function(errorMessage) {
            alert(errorMessage.responseText);
        }
    });
}

function fillDropDownList(elementId, result) {
    window.$("#" + elementId).children("option:not(:first)").remove();

    window.$.each(result,
        function(key, data) {
            var option = new Option();

            window.$(option).val(data.id);
            window.$(option).html(data.description);
            window.$("#" + elementId).append(option);
        });
}

function checkIfValueExists(elementId, id) {
    var options = window.$("#" + elementId + " option");

    for (var i = 0; i < options.length; i++) {
        if (options[i].value === id.toString()) {
            return true;
        }
    }
    return false;
}

function appendNewOption(elementId, id, description) {
    var option = new Option();

    window.$(option).val(id);
    window.$(option).html(description);
    window.$("#" + elementId).append(option);
}

function redirectToIndex(e, controller) {
    if ((e.type == "create" || e.type == "update" || e.type == "destroy") && !e.response.modelState) {
        //var data = e.response.Data[0];
        //window.location.href = "@(Url.Action("Index", "Size"))";
        window.location.href = "/" + controller + "/Index";
    }
}