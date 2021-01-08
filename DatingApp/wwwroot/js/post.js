
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

    $("#PostWall").on("keyup", "#PostText", AdjustCounter);

    function AdjustCounter() {
        var number = $("#PostText").val().length;
        if (number <= 0) {
            if ($("#CreatePostCard").hasClass("border-success")) {
                $("#CreatePostCard").removeClass("border-success");
            }
            if (!$("#CreatePostCard").hasClass("border-danger")) {
                $("#CreatePostCard").addClass("border-danger");
            }
        }
        else if (number < 250) {
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
        if (number >= 250) {
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
        if (number > 280) {
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
        $("#TextAreaWordCounter").text(300 - number);
    }