
$("#PostWall").on("click", "#SubmitPost", AddPost);

//Hämtar värdet användaren skriver in samt vilken profil användaren kollar på.
//Skickar med dessa värden till metoden AddPost (i PostApiController) där en ny post läggs till.
//Uppdaterar sedan sidan för att kunna se den nya posten. 
    function AddPost() {
        if ($("#PostText").val() != "" && $("#PostText").val().length <= 300) {
            var post;
            var id = $("#PersonId").val();
            var text = $("#PostText").val();

            post = { PostText: text, PersonId: id };
            $.ajax({
                type: "POST",
                url: "/api/PostApi/AddPost",
                data: JSON.stringify(post),
                contentType: "application/json;charset=UTF-8",
                //success: function (data) {
                //    location.reload(true);
                success: () => {
                    UpdatePostwall();
                },
                error: () => {
                    alert("Error: Failure to add post");
                }
            });
        } else {
            alert("Your post needs to consist of between 1 and 300 characters.")
        }
}

//Uppdaterar partial view _Post
function UpdatePostwall() {
    var id = $("#PersonId").val();

    var serviceUrl = "/Person/UpdatePostWall/" + id;
    var request = $.post(serviceUrl);
    request.done(function (data) {
        $("#PostWall").html(data);
    }).fail(() => {
        console.log("Error: Failure to update post wall");
    });
}


$("#PostWall").on("keyup", "#PostText", Counter);

//Räknar hur många tecken som skrivits när användaren skriver en ny post. 
//Om antalet tecken är fler än 250 varnas användaren genom att rutan som visar antelet tecken blir gul.
//Om antalet tecken är fler än 280 varnas användaren genom att rutan som visar antelet tecken blir röd.
    function Counter() {
        var numberOfCharacters = $("#PostText").val().length;
        if (numberOfCharacters <= 0) { 
            if ($("#CreatePostCard").hasClass("border-success")) {
                $("#CreatePostCard").removeClass("border-success");
            }
            if (!$("#CreatePostCard").hasClass("border-danger")) {
                $("#CreatePostCard").addClass("border-danger");
            }
        }
        else if (numberOfCharacters < 250) {
            if ($("#TextAreaWordCounter").hasClass("badge-danger")) {
                $("#TextAreaWordCounter").removeClass("badge-danger");
            }
            if ($("#TextAreaWordCounter").hasClass("badge-warning")) {
                $("#TextAreaWordCounter").removeClass("badge-warning");
            }
            if (!$("#TextAreaWordCounter").hasClass("badge-success")) {
                $("#TextAreaWordCounter").addClass("badge-success");
            }
            if ($("#CreatePostCard").hasClass("border-danger")) {
                $("#CreatePostCard").removeClass("border-danger");
            }
            if (!$("#CreatePostCard").hasClass("border-success")) {
                $("#CreatePostCard").addClass("border-success");
            }
        }
        if (numberOfCharacters >= 250) {
            if ($("#TextAreaWordCounter").hasClass("badge-danger")) {
                $("#TextAreaWordCounter").removeClass("badge-danger");
            }
            if ($("#TextAreaWordCounter").hasClass("badge-success")) {
                $("#TextAreaWordCounter").removeClass("badge-success");
            }
            if (!$("#TextAreaWordCounter").hasClass("badge-warning")) {
                $("#TextAreaWordCounter").addClass("badge-warning");
            }
            if ($("#CreatePostCard").hasClass("border-danger")) {
                $("#CreatePostCard").removeClass("border-danger");
            }
            if (!$("#CreatePostCard").hasClass("border-success")) {
                $("#CreatePostCard").addClass("border-success");
            }
        }
        if (numberOfCharacters > 280) {
            if ($("#TextAreaWordCounter").hasClass("badge-warning")) {
                $("#TextAreaWordCounter").removeClass("badge-warning");
            }
            if ($("#TextAreaWordCounter").hasClass("badge-success")) {
                $("#TextAreaWordCounter").removeClass("badge-success");
            }
            if (!$("#TextAreaWordCounter").hasClass("badge-danger")) {
                $("#TextAreaWordCounter").addClass("badge-danger");
            }
            if ($("#CreatePostCard").hasClass("border-success")) {
                $("#CreatePostCard").removeClass("border-success");
            }
            if (!$("#CreatePostCard").hasClass("border-danger")) {
                $("#CreatePostCard").addClass("border-danger");
            }
        }
        $("#TextAreaWordCounter").text(300 - numberOfCharacters);
    }