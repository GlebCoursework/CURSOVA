function Buy(item) {
    $.ajax({
        type: "GET",
        url: "/Home/Buy",
        data: { id: item },
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,
        success: function (result) {
            $("#MyDiv").html(result);
        }
    });
}

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Home/Buy",
        data: { id: 0 },
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,
        success: function (result) {
            $("#MyDiv").html(result);
        }
    });
});

function Del(item) {
    $.ajax({
        type: "GET",
        url: "/Home/Del",
        data: { id: item },
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,
        success: function (result) {
            $("#MyDiv").html(result);
        }
    });
}