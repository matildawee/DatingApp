
$("#myProfile").on("click", "#editProfile", UpdateProfile);

function UpdateProfile() {
    var firstname = $("#firstName").val();
    var lastname = $("#lastName").val();
    var description = $("#description").val();
    var person;
    person = { FirstName: firstname, LastName: lastname, Description: description };
    
    var hej;
    $.ajax({
        type: "POST",
        url: "/Person/UpdateProfile",
        data: JSON.stringify(person),
        //dataType: 'json',
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            location.reload(true);
        },
        error: () => {
            alert("Error: Failure to add post");
        }
    });
}