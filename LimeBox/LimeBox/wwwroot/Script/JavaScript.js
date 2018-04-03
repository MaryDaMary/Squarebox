//$("data-simplebar2").click(function (event) {
//    var target = $(event.target);
//    target.fadeTo("slow", 0.5);
//});


//$("ShowBox").click(() => {
//    alert("test");
//    $(this).fadeTo("slow", 0.33)
//    $("#ShowBox").fadeTo("slow", 0.5);
//})

function AddToCart(id) {
    $.ajax({
        url: '/Cart/AddToCart',
        data: { "id": id },
        success: function (msg) {
            alert("Success");
            //$("#divResult").html("success");
        },
        error: function (e) {
            console.log(e);
            alert("Something went wrong!")
            //$("#divResult").html("Something Wrong.");
        }
    });
};

function RemoveFromCart(id) {
    $.ajax({
        url: '/Cart/RemoveFromCart',
        data: { "id": id },
        success: function (msg) {
            alert("Success!")
            location.reload();
            //$("#divResult").html("success");
        },
        error: function (e) {
            alert("Something went wrong!")
            //$("#divResult").html("Something Wrong.");
        }
    });
};