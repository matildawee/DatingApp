

    $("#SubmitPost").click(function () {
        //e.preventDefault();
        $("#TEST").hide();
                var post = new Post();
                post.Person = Model.PersonId;
                post.Author = $(User).val();
                post.PostText = $('#PostText').val();
                $.ajax({
            url: "/PostsApiController/PostPost/",
            type: "POST",
            data: post,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: alert("sent")
        });
    });
       