$(document).on("click", "#ShowBox", function () {
    $(this).parent(this).addClass("SelectedBox");
    $(this).parent(this).fadeTo("slow", 0.5);
});

$(document).on("mouseover", ".OuterBox", function () {
    $(this).css("box-shadow", "0 0 10px #292828");
});
$(document).on("mouseout", ".OuterBox", function () {
    $(this).css("box-shadow", "none");
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
