using System.Data.Entity.Migrations;

namespace LunaSkypeBot.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DerpyImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DerpyImageId = c.Long(nullable: false),
                        IdHash = c.String(maxLength: 2147483647),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        DbUpdatedAt = c.DateTime(nullable: false),
                        FileName = c.String(maxLength: 2147483647),
                        Description = c.String(maxLength: 2147483647),
                        Uploader = c.String(maxLength: 2147483647),
                        Image = c.String(maxLength: 2147483647),
                        Score = c.Long(nullable: false),
                        Upvotes = c.Long(nullable: false),
                        Downvotes = c.Long(nullable: false),
                        Favourites = c.Long(nullable: false),
                        CommentCount = c.Long(nullable: false),
                        Tags = c.String(maxLength: 2147483647),
                        Width = c.Long(nullable: false),
                        Height = c.Long(nullable: false),
                        AspectRatio = c.Double(nullable: false),
                        OriginalFormat = c.String(maxLength: 2147483647),
                        MimeType = c.String(maxLength: 2147483647),
                        Sha512Hash = c.String(maxLength: 2147483647),
                        OriginalSha512Hash = c.String(maxLength: 2147483647),
                        SourceUrl = c.String(maxLength: 2147483647),
                        License = c.String(maxLength: 2147483647),
                        RepresentationsId = c.Long(nullable: false),
                        IsRendered = c.Boolean(nullable: false),
                        IsOptimized = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DerpyRepresentations", t => t.RepresentationsId, cascadeDelete: true)
                .Index(t => t.DerpyImageId, unique: true)
                .Index(t => t.RepresentationsId);
            
            CreateTable(
                "dbo.DerpyRepresentations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DerpyRepresentationId = c.Long(nullable: false),
                        ThumbTiny = c.String(maxLength: 2147483647),
                        ThumbSmall = c.String(maxLength: 2147483647),
                        Thumb = c.String(maxLength: 2147483647),
                        Small = c.String(maxLength: 2147483647),
                        Medium = c.String(maxLength: 2147483647),
                        Large = c.String(maxLength: 2147483647),
                        Tall = c.String(maxLength: 2147483647),
                        Full = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.DerpyRepresentationId, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DerpyImages", "RepresentationsId", "dbo.DerpyRepresentations");
            DropIndex("dbo.DerpyRepresentations", new[] { "DerpyRepresentationId" });
            DropIndex("dbo.DerpyImages", new[] { "RepresentationsId" });
            DropIndex("dbo.DerpyImages", new[] { "DerpyImageId" });
            DropTable("dbo.DerpyRepresentations");
            DropTable("dbo.DerpyImages");
        }
    }
}
