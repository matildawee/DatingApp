

//$("#SubmitPost").click(function () {
//    //e.preventDefault();

//    var post;
//    var id = 1;
//    var text = "neeej";
//    post = { PostText: text, PersonId: id };
//    //post.Author = 2;
//    //post.Post = "neej";
//    //post.Person = Model.PersonId;
//    //post.Author = $(User).val();
//    //post.PostText = $('#PostText').val();
//    $("#TEST").hide();
//    $.ajax({
//        type: "POST",
//        url: "/api/PostsApi/",        
//        data: JSON.stringify(post),
//        contentType: "application/json; charset=utf-8",
//        //dataType: "json",
//        success: alert("Success!"),
//        error: () => {
//            alert("Error: Failed to add new post");
//        }
//    });
//});

$("#PostWall").on("click", "#SubmitPost", AddPost);

function AddPost() {
    if ($("#PostText").val() != "" && $("#PostText").val().length <= 280) {
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