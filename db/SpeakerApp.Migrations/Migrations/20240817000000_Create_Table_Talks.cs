using FluentMigrator;

namespace SpeakerApp.Migrations
{
    [Migration(20240817000000)]
    public class CreateTableTalks : Migration
    {
        public override void Up()
        {
            Create.Table("Talks")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Title").AsString(100).NotNullable()
                .WithColumn("Abstract").AsString(500).NotNullable()
                .WithColumn("SpeakerId").AsGuid().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable();

            Insert.IntoTable("Talks")
                .Row(new
                {
                    Id = Guid.Parse("1f18b3a2-c716-4f1f-902e-e57ec37f6260"),
                    Title = "Sample Talk",
                    Abstract = "This is a sample talk",
                    SpeakerId = Guid.Parse("f4b3b2b1-2f8d-4b9b-8b1e-7b0e4e3d2c1a"),
                    CreatedAt = DateTime.Now
                });
        }

        public override void Down()
        {
            Delete.Table("Talks");
        }
    }
}