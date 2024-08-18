using FluentMigrator;

namespace SpeakerApp.Migrations
{
    [Migration(20240721000000)]
    public class CreateSpeakerTable : Migration
    {
        public override void Up()
        {
            // Create the Speakers table
            Create.Table("Speakers")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("FirstName").AsString(100).NotNullable()
                .WithColumn("Lastname").AsString(100).NotNullable()
                .WithColumn("Email").AsString(100).NotNullable()
                .WithColumn("Bio").AsString(250).NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable();

            // Insert some sample data
            Insert.IntoTable("Speakers")
                .Row(new
                {
                    Id = Guid.Parse("f4b3b2b1-2f8d-4b9b-8b1e-7b0e4e3d2c1a"),
                    FirstName = "Russell",
                    LastName = "Durham",
                    Email = "russell.durham@improving.com",
                    Bio = "Russell is a software developer with 10+ years of experience.",
                    CreatedAt = System.DateTime.UtcNow
                });
        }

        public override void Down()
        {
            // Drop the Speakers table
            Delete.Table("Speakers");
        }
    }
}