
$("#myProfile").on("click", "#editProfile", UpdateProfile);

function UpdateProfile() {
    var firstname = $("#firstName").val();
    var lastname = $("#lastName").val();
    var description = $("#description").val();
    var post;
    post = { FirstName: firstname, LastName: lastname, Description: description };
    $.ajax({
        contentType: "application/json;charset=UTF-8",
        //dataType: 'json',
        type: "POST",
        url: "/Person/UpdateProfile",
        data: JSON.stringify(post),
        //cache: false,
        
        success: function (data) {
            location.reload(true);
        },
        error: () => {
            alert("Error: Failure to add post");
        }
    });
}