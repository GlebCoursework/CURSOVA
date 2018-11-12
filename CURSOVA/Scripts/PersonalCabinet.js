function GetChangePassword() {
    $.ajax({
        type: "GET",
        url: "/Account/GetChangePassword",
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,
        success: function (result) {
            $("#Div-PersonalCabinet").html(result);
        }
    });
    return false;
}

function ChangePassword(button) {

    var $form = $(button).parents('form');
    $.ajax({
        //type: "POST",
        url: "/Account/ChangePassword",
        data: $form.serialize(),
        cache: false,
        async: true,
        success: function (result) {
            if (result === "") {
                $.ajax({

                    url: "/Account/ButtonsForChange",
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    async: true,
                    success: function (result) {
                        $("#Div-PersonalCabinet").html(result);
                        $("#labelforpasswordchange").text("Password was changed successfully");
                    }
                });
            }
            else {
                $("#Div-PersonalCabinet").html(result);
            }
        }
    });
    return false;
}

function GetChangeEmail() {
    $.ajax({
        type: "GET",
        url: "/Account/GetChangeEmail",
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,
        success: function (result) {
            $("#Div-PersonalCabinet").html(result);
        }
    });
}

function ChangeEmail(button) {
    var $form = $(button).parents('form');
    $.ajax({
        url: "/Account/ChangeEmail",
        data: $form.serialize(),
        cache: false,
        async: true,
        success: function (result) {
            if (result === "") {
                $.ajax({
                    url: "/Account/ButtonsForChange",
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    async: true,
                    success: function (result) {
                        $("#Div-PersonalCabinet").html(result);
                        $("#labelforemailchange").text("Email was changed successfully");
                    }
                });
            }
            else {
                $("Div-PersonalCabinet").html(result);
            }
        }
    });
    return false;
}