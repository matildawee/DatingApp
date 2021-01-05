// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$(document).ready(() => {
//    ShowPopUp();
//    ShowPopUp();
//});

//$("#popupRequests").on("click", "#AcceptBtn", AcceptRequest);
//$("#popupRequests").on("click", "#DeclineBtn", DeclineRequest);

var ref = $('#showRequests');
var popup = $('#popupRequests');
popup.hide();

//ref.click(function () {
//    popup.show();
//});

ref.click(function ShowPopUp() {
    popup.show();
    var popper = new Popper(ref, popup, {
        placement: 'bottom',
        modifiers: {
            offset: {
                enabled: true,
                offset: "0, 10"
            }
        }
    });
    Update_Content();
});

function Update_Content() {
    $.ajax({
        type: "GET",
        url: "/Request/GetFriendRequests/",
        contentType: "application/json;charset=UTF-8",
        dataType: "html",
        success: function (data) {
            $("#popupRequests").html(data);
        },
        error: () => {
            alert("Error: Unable to fetch and display friend requests");
        }
    });
}

function AcceptRequest() {
    var UserId = this.attributes[1].value;
    $.ajax({
        type: "POST",
        url: "/Request/AcceptRequest/" + UserId,
        dataType: "JSON",
        success: () => {
            Update_Content();
            //SetNumberOfNotifications();
            //var currentUrl = window.location.href;
            //var urlArray = currentUrl.split("/Profile/Index/");
            //if (urlArray.length > 1) {
            //    if (urlArray[1] == UserId) {
            //        ButtonGroupFriends();
            //    }
            //    Update_Friends();
            //}
            //if ($("#NotificationNumberSpan").val() == 0) {
            //    ToggleNotificationPopUpDivDisplay();
            //}
        },
        error: () => {
            alert("Error: Unable to accept friend request.");
        }
    });
}