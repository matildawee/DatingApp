
$("#myProfile").on("click", "#editProfile", UpdateProfile);

function UpdateProfile() {
    var firstname = $("#firstName").val();
    var lastname = $("#lastName").val();
    var description = $("#description").val();
    var accountHidden;
    if ($('#accountHidden').is(":checked")) {
        accountHidden = true
    }
    else {
        accountHidden = false;
    }
    var post;
    post = { FirstName: firstname, LastName: lastname, Description: description, AccountHidden: accountHidden }; 
    $.ajax({
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        type: "POST",
        url: "/api/PostApi/updateprofile",
        data: JSON.stringify(post),
        catch: false,
        success:                                 //() => { alert("Success"); 
            function (data) {
            location.reload(true);
        },
        error: () => {
           alert("Error: Failure to update profile :(");
        }
    });
}