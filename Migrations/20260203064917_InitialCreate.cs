using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORY",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORY", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "INQUIRY",
                columns: table => new
                {
                    InquiryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Subject = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    InquiryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, defaultValue: "Pending"),
                    ResponseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Response = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INQUIRY", x => x.InquiryID);
                });

            migrationBuilder.CreateTable(
                name: "MEMBER",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    JoinDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEMBER", x => x.MemberID);
                });

            migrationBuilder.CreateTable(
                name: "VENUE",
                columns: table => new
                {
                    VenueID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VenueName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    Facilities = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ContactPhone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    ContactEmail = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VENUE", x => x.VenueID);
                });

            migrationBuilder.CreateTable(
                name: "PREFERENCE",
                columns: table => new
                {
                    PreferenceID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberID = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PREFERENCE", x => x.PreferenceID);
                    table.ForeignKey(
                        name: "FK_PREFERENCE_CATEGORY_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "CATEGORY",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PREFERENCE_MEMBER_MemberID",
                        column: x => x.MemberID,
                        principalTable: "MEMBER",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EVENT",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VenueID = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    EventName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EventDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AvailableSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, defaultValue: "Upcoming"),
                    ImageURL = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVENT", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_EVENT_CATEGORY_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "CATEGORY",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVENT_VENUE_VenueID",
                        column: x => x.VenueID,
                        principalTable: "VENUE",
                        principalColumn: "VenueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BOOKING",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberID = table.Column<int>(type: "INTEGER", nullable: false),
                    EventID = table.Column<int>(type: "INTEGER", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalTickets = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, defaultValue: "Confirmed"),
                    BookingReference = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOOKING", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_BOOKING_EVENT_EventID",
                        column: x => x.EventID,
                        principalTable: "EVENT",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BOOKING_MEMBER_MemberID",
                        column: x => x.MemberID,
                        principalTable: "MEMBER",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REVIEW",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberID = table.Column<int>(type: "INTEGER", nullable: false),
                    EventID = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REVIEW", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_REVIEW_EVENT_EventID",
                        column: x => x.EventID,
                        principalTable: "EVENT",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REVIEW_MEMBER_MemberID",
                        column: x => x.MemberID,
                        principalTable: "MEMBER",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BOOKING_DETAIL",
                columns: table => new
                {
                    DetailID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookingID = table.Column<int>(type: "INTEGER", nullable: false),
                    SeatType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOOKING_DETAIL", x => x.DetailID);
                    table.ForeignKey(
                        name: "FK_BOOKING_DETAIL_BOOKING_BookingID",
                        column: x => x.BookingID,
                        principalTable: "BOOKING",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BOOKING_BookingReference",
                table: "BOOKING",
                column: "BookingReference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BOOKING_EventID",
                table: "BOOKING",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_BOOKING_MemberID",
                table: "BOOKING",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_BOOKING_DETAIL_BookingID",
                table: "BOOKING_DETAIL",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORY_CategoryName",
                table: "CATEGORY",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EVENT_CategoryID",
                table: "EVENT",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_EVENT_VenueID",
                table: "EVENT",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_MEMBER_Email",
                table: "MEMBER",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PREFERENCE_CategoryID",
                table: "PREFERENCE",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PREFERENCE_MemberID_CategoryID",
                table: "PREFERENCE",
                columns: new[] { "MemberID", "CategoryID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_EventID",
                table: "REVIEW",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_MemberID",
                table: "REVIEW",
                column: "MemberID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BOOKING_DETAIL");

            migrationBuilder.DropTable(
                name: "INQUIRY");

            migrationBuilder.DropTable(
                name: "PREFERENCE");

            migrationBuilder.DropTable(
                name: "REVIEW");

            migrationBuilder.DropTable(
                name: "BOOKING");

            migrationBuilder.DropTable(
                name: "EVENT");

            migrationBuilder.DropTable(
                name: "MEMBER");

            migrationBuilder.DropTable(
                name: "CATEGORY");

            migrationBuilder.DropTable(
                name: "VENUE");
        }
    }
}
