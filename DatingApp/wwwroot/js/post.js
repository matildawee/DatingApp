
$("#PostWall").on("click", "#SubmitPost", AddPost);

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
            success: function (data) {
                location.reload(true);
            },
            error: () => {
                alert("Error: Failure to add post");
            }
        });
    } else {
        alert("Your post needs to consist of between 1 and 300 characters.")
    }
}