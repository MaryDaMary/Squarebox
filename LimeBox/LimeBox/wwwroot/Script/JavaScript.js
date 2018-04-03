

$("#RemovFromCart").click(() => {
    $.ajax({
        url: 'Cart/RemoveFromCart',
        data: { "id": "1" },
        success: function (msg) {
            $("#divResult").html("success");
        },
        error: function (e) {
            $("#divResult").html("Something Wrong.");
        }
    });
});

$("#AddToCart").click(() => {
    $.ajax({
        url: 'Cart/AddToCart',
        data: id,
        success: function (msg) {
            $("#divResult").html("success");
        },
        error: function (e) {
            $("#divResult").html("Something Wrong.");
        }
    });
});