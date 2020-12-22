using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class UpdateAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Users_RecevierPersonId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Users_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_FirstUserId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_SecondUserId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_PersonId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friends",
                table: "Friends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Person");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameTable(
                name: "Friends",
                newName: "Friend");

            migrationBuilder.RenameTable(
                name: "FriendRequests",
                newName: "FriendRequest");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_PersonId",
                table: "Post",
                newName: "IX_Post_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_AuthorId",
                table: "Post",
                newName: "IX_Post_AuthorId");

            migrationBuilder.RenameColumn(
                name: "SecondUserId",
                table: "Friend",
                newName: "SecondPersonId");

            migrationBuilder.RenameColumn(
                name: "FirstUserId",
                table: "Friend",
                newName: "FirstPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_SecondUserId",
                table: "Friend",
                newName: "IX_Friend_SecondPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequest",
                newName: "IX_FriendRequest_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_RecevierPersonId",
                table: "FriendRequest",
                newName: "IX_FriendRequest_RecevierPersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friend",
                table: "Friend",
                column: "FirstPersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequest",
                table: "FriendRequest",
                column: "FriendRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_Person_FirstPersonId",
                table: "Friend",
                column: "FirstPersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_Person_SecondPersonId",
                table: "Friend",
                column: "SecondPersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_Person_RecevierPersonId",
                table: "FriendRequest",
                column: "RecevierPersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_Person_SenderId",
                table: "FriendRequest",
                column: "SenderId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Person_AuthorId",
                table: "Post",
                column: "AuthorId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Person_PersonId",
                table: "Post",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_Person_FirstPersonId",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_Person_SecondPersonId",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_Person_RecevierPersonId",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_Person_SenderId",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Person_AuthorId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Person_PersonId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequest",
                table: "FriendRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friend",
                table: "Friend");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "FriendRequest",
                newName: "FriendRequests");

            migrationBuilder.RenameTable(
                name: "Friend",
                newName: "Friends");

            migrationBuilder.RenameIndex(
                name: "IX_Post_PersonId",
                table: "Posts",
                newName: "IX_Posts_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_AuthorId",
                table: "Posts",
                newName: "IX_Posts_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequest_SenderId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequest_RecevierPersonId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_RecevierPersonId");

            migrationBuilder.RenameColumn(
                name: "SecondPersonId",
                table: "Friends",
                newName: "SecondUserId");

            migrationBuilder.RenameColumn(
                name: "FirstPersonId",
                table: "Friends",
                newName: "FirstUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friend_SecondPersonId",
                table: "Friends",
                newName: "IX_Friends_SecondUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                column: "FriendRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friends",
                table: "Friends",
                column: "FirstUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Users_RecevierPersonId",
                table: "FriendRequests",
                column: "RecevierPersonId",
                principalTable: "Users",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Users_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_FirstUserId",
                table: "Friends",
                column: "FirstUserId",
                principalTable: "Users",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_SecondUserId",
                table: "Friends",
                column: "SecondUserId",
                principalTable: "Users",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_PersonId",
                table: "Posts",
                column: "PersonId",
                principalTable: "Users",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
