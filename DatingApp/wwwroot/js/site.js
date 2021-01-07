// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// -- Friend Request -- 

//Getting and setting the number of friend requests when the page has been loaded.
$(document).ready(function () {
    setInterval(showNumberOfRequests, 5000);
    function showNumberOfRequests() {;
        SetNumberOfRequests();
    }
});

$("#popupRequests").on("click", "#AcceptBtn", AcceptRequest);
$("#popupRequests").on("click", "#DeclineBtn", DeclineRequest);

var ref = $('#showRequests');
var popup = $('#popupRequests');

//Hiding the popup div and friedn requests notification as default
popup.hide();
$("#requestQty").hide();

/*If the friend requests button is clicked and the popup div is hidden, it shows up. A popper is used for positioning the div
 * underneath the button. Methods for updating the content and getting number of requests is called. Otherwise the popup div
 * will be hidden.*/
ref.click(function ShowPopUp() {
    if ($(popup).is(":hidden")) {
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
        SetNumberOfRequests();
    }
    else {
        popup.hide();
    }
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

function SetNumberOfRequests() {
    $.ajax({
        type: "POST",
        url: "/Request/GetNumberOfRequests/",
        datatype: "JSON",
        success: function (data) {
            var number = data.data;
            if (number >= 1) {
                $("#requestQty").show();
                $("#requestQty").text(number); // Set number
            }
            else {
                $("#requestQty").hide();
            }
        },
        error: () => {
            alert("Error: Unable to fetch and display number of friend requests");
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
            SetNumberOfRequests();
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

function DeclineRequest() {
    var UserId = this.attributes[1].value;
    $.ajax({
        type: "POST",
        url: "/Request/DeclineRequest/" + UserId,
        dataType: "JSON",
        success: () => {
            Update_Content();
            SetNumberOfRequests();

            //var currentUrl = window.location.href;
            //var urlArray = currentUrl.split("/Profile/Index/");
            //if (urlArray.length > 1) {
            //    if (urlArray[1] == UserId) {
            //        ButtonGroupNotFriends();
            //    }
            //}

            //if ($("#NotificationNumberSpan").val() == 0) {
            //    ToggleNotificationPopUpDivDisplay();
            //}
        },
        error: () => {
            alert("Error: Unable to decline friend request.");
        }
    });
}

$(document).ready(function () {
    //var d = new Date();
    //if (d.getMonth() == 5) {
    //    $("#friendStatus").text("tjoho");
    //}
    //else {
    //    $("#friendStatus").text("nehee");
    //}
    testing();
});

function testing() {
    $.ajax({
        type: "POST",
        url: "/Request/GetFriendStatus/",
        dataType: "JSON",
        success: function (data) {
            var result = data.text;
            //$("#friendStatus").text(result);
            alert(result);
        },
        error: function (data) {
            var r = data.text;
            alert("Error: " + r);
        }
    });
}
