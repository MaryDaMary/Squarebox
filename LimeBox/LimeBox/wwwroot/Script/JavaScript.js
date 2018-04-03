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
        },
        error: function (e) {
            console.log(e);
            alert("Something went wrong!")
        }
    });
};

function RemoveFromCart(id) {
    $.ajax({
        url: '/Cart/RemoveFromCart',
        data: { "id": id },
        success: function (msg) {
            location.reload();
        },
        error: function (e) {
            alert("Something went wrong!")
        }
    });
};