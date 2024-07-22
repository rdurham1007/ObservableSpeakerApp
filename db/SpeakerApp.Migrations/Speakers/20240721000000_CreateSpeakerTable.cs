using FluentMigrator;

namespace SpeakerApp.Migrations.Speakers
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
        }

        public override void Down()
        {
            // Drop the Speakers table
            Delete.Table("Speakers");
        }
    }
}