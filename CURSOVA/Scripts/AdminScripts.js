﻿function EditPizza(item) {
    $.ajax({

        url: "Admin/Home/EditPizza",
        contentType: "application/json; charset=utf-8",
        data: { id: item },
        cache: false,
        async: true,
        success: function (result) {
            $("#Div-Edit").html(result);
        }
    });
    return false;
}