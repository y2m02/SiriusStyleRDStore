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

$(function () {
    window.$("input").attr("autocomplete", "off");
});