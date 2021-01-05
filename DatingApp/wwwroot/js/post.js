

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
    //var currentUrl = window.location.href;
    //var urlArray = currentUrl.split("/Profile/Index/");
    //var Id = "";
    if ($("#PostText").val() != "" && $("#PostText").val().length <= 280) {
        var post;
        //if (urlArray.length >= 2) {
        //    Id = urlArray[1];
        //}
        //if (!Id == "") {
        //    post = { Text: $("#PostText").val(), PostToId: Id };
        //} else {
        //    post = { Text: $("#PostText").val() };
        //}
        var id = 1;
        var text = "neeej";
        post = { PostText: text, PersonId: id };
        var hej;
        $.ajax({
            type: "POST",
            url: "/PostApi/AddPost",
            data: JSON.stringify(post),
            contentType: "application/json;charset=UTF-8",
            success: () => {
                //Update_Wall();
                alert(":O");
            },
            error: () => {
                alert("Error: Failure to add post");
            }
        });
    } else {
        alert("Your post needs to consist of between 1 and 280 characters.")
    }
}