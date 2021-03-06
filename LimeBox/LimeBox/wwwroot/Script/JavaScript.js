﻿$(document).on("click", "#ShowBox", function () {
    $(this).parent(this).addClass("SelectedBox");
    $(this).parent(this).fadeTo("slow", 0.5);
});

$(document).on("mouseover", ".OuterBox", function () {
    $(this).css("box-shadow", "0 0 10px #292828");
});
$(document).on("mouseout", ".OuterBox", function () {
    $(this).css("box-shadow", "none");
});

$("#OrderTable").on("mouseover", "tr", function () {
    $(this).css("background-color", "#292828");
});
$("#OrderTable").on("mouseout", "tr", function () {
    $(this).css("background-color", "");
});


function SelectOrder(id, object) {
    let element = $(object);
    element.parent().find("tr:nth-child(2)").toggle();
    $.ajax({
        url: '/Admin/Order',
        data: { "id": id },
        success: function (result) {
            element.parent().find("#Content").html(result);
        },
        error: function (e) {
            console.log(e);
            alert("Something went wrong!");
        }
    });
}

function ChangeStatus(element, orderId) {
    var selectedTab = element.selectedOptions[0];
    $.ajax({
        url: '/Admin/ChangeOrderStatus',
        data: { id: orderId, status: selectedTab.index },
        success: function (result) {
            $(element).parentsUntil("tbody").parent().first("tbody").first("tr").find("td:nth-child(3)").html(selectedTab.text);
        },
        error: function (e) {
            console.log(e);
            alert("Something went wrong!");
        }
    });
}


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
