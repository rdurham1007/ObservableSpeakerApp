using FluentMigrator;

namespace SpeakerApp.Migrations
{
    [Migration(20240818000000)]
    public class CreateTablePresentations : Migration
    {
        public override void Up()
        {
            Create.Table("Presentations")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("ScheduledAt").AsDateTime().NotNullable()
                .WithColumn("Location").AsString()
                .WithColumn("SpeakerId").AsGuid().NotNullable()
                .WithColumn("SpeakerName").AsString().NotNullable()
                .WithColumn("SpeakerBio").AsString().Nullable()
                .WithColumn("TalkId").AsGuid().NotNullable()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Abstract").AsString().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable();

            Insert.IntoTable("Presentations")
                .Row(new
                {
                    Id = Guid.NewGuid(),
                    ScheduledAt = DateTime.UtcNow.AddDays(3),
                    Location = "New York",
                    SpeakerId = Guid.Parse("f4b3b2b1-2f8d-4b9b-8b1e-7b0e4e3d2c1a"),
                    SpeakerName = "Russell Durham",
                    SpeakerBio = "Russell is a software developer with 10+ years of experience.",
                    TalkId = Guid.Parse("1f18b3a2-c716-4f1f-902e-e57ec37f6260"),
                    Title = "Sample Talk",
                    Abstract = "This is a sample talk",
                    CreatedAt = DateTime.UtcNow
                });
                
        }

        public override void Down()
        {
            Delete.Table("Presentations");
        }
    }
}