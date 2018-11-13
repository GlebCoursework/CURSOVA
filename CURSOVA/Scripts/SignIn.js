function LoginModel(btnClicked)
{
    var $form = $(btnClicked).parents('form');
    $.ajax({
        type: "POST",
        url: "/Account/LogInPost",
        data: $form.serialize(),
        cache: false,
        async: true,
        success: function (result) {       
            if (result === "") {
                document.location.reload(true)
            }
            else {
                $("#LoginDiv").html(result);
            }
        }
    });
    return false;
}

function Login() {
    $.ajax({
        type: "GET",
        url: "/Account/LogIn",
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,
        success: function (result) {
            $("#LoginDiv").html(result);
        }
    });
    return false;
}


function Registration(btnClicked) {
    var $form = $(btnClicked).parents('form');
    $.ajax({
        //type: "POST",
        url: "/Account/Registration",
        data: $form.serialize(),
        cache: false,
        async: true,
        success: function (result) {
            if (result === "") {
                $.ajax({
                    type: "GET",
                    url: "/Account/LogIn",
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    async: true,
                    success: function (result) {
                        $("#LoginDiv").html(result);
                    }
                });
            }
            else {
                $("#LoginDiv").html(result);
            }
        }
    });
    return false;
}

function GetRegistration() {
    $.ajax({
        type: "GET",
        url: "/Account/GetRegistration",
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,
        success: function (result) {
            $("#LoginDiv").html(result);
        }
    });
    return false;
}
