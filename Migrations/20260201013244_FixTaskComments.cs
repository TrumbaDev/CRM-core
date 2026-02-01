using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmCore.Migrations
{
    /// <inheritdoc />
    public partial class FixTaskComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_task_comments_TaskId_UserId",
                table: "task_comments");

            migrationBuilder.CreateIndex(
                name: "IX_task_comments_TaskId_UserId",
                table: "task_comments",
                columns: new[] { "TaskId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_task_comments_TaskId_UserId",
                table: "task_comments");

            migrationBuilder.CreateIndex(
                name: "IX_task_comments_TaskId_UserId",
                table: "task_comments",
                columns: new[] { "TaskId", "UserId" },
                unique: true);
        }
    }
}
