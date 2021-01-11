// -- Friend Request -- 

//Hämtar och sätter antalet vänförfrågningar när sidan laddats in.
$(document).ready(function () {
    setInterval(showNumberOfRequests, 5000);
    function showNumberOfRequests() {;
        SetNumberOfRequests();
    }
});


var ref = $('#showRequests');
var popup = $('#popupRequests');

//Diven som visas med hjälp av en popper och notifikationen för antalet förfrågningar är gömda som default.
popup.hide();
$("#requestQty").hide();

/*Om knappen för förfrågningar klickas på och popup-diven är gömd visas den. Poppern gör att popup-diven hamnar under knappen.
 * Metoderna för att uppdatera innehållet i diven och antalet förfrågningar anropas. Visas diven när användaren trycker på knappen
 * göms den istället.*/
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
            $("#popupRequests").html(data); //Uppdaterar diven med html (partial view)
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
                $("#requestQty").text(number); // Sätter antalet förfrågningar.
            }
            else {
                $("#requestQty").hide(); //Gömmer notifikationen om det inte finns några förfrågningar.
            }
        },
        error: () => {
            console.log("Error: Unable to fetch and display number of friend requests");
        }
    });
}


// -- Sök-funktionen --

//Submita sökknappen när användaren trycker på enter-knappen inuti input-fältet. 
    $(document).ready(function () {
        $("#search").keypress(function (event) {
            if (event.which == 13) {
                event.preventDefault();
                $("#searchBtn").click();
            }
        });
        });
