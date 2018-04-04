$(document).on("click", "#ShowBox", function () {
    $(this).fadeTo("slow", 0.5);
});


function AddToCart(id) {
    $.ajax({
        url: '/Cart/AddToCart',
        data: { "id": id },
        success: function (msg) {
        },
        error: function (e) {
            console.log(e);
            alert("Something went wrong!");
        }
    });
}

function RemoveFromCart(id) {
    $.ajax({
        url: '/Cart/RemoveFromCart',
        data: { "id": id },
        success: function (msg) {
            location.reload();
        },
        error: function (e) {
            alert("Something went wrong!");
        }
    });
}