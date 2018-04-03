

function RemoveFromCart(id) {
    alert("Test")
    $.ajax({
        url: '/Cart/RemoveFromCart',
        data: { "id": id },
        success: function (msg) {
            alert("Success!")
            //$("#divResult").html("success");
        },
        error: function (e) {
            alert("Something went wrong!")
            //$("#divResult").html("Something Wrong.");
        }
    });
};

function AddToCart(id) {
    $.ajax({
        url: '/Cart/AddToCart',
        data: { "id": id },
        success: function (msg) {
            alert("Success!")
            //$("#divResult").html("success");
        },
        error: function (e) {
            console.log(e);
            alert("Something went wrong!")
            //$("#divResult").html("Something Wrong.");
        }
    });
};