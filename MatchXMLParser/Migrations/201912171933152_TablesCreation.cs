namespace MatchXMLParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablesCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ENETSCORES.Corners",
                c => new
                    {
                        ExternalId = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "ENETSCORES.Crosses",
                c => new
                    {
                        ExternalId = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "ENETSCORES.Goals",
                c => new
                    {
                        ExternalId = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        Minute = c.String(),
                        ScorerId = c.String(),
                        AssistId = c.String(),
                        Type = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "ENETSCORES.Matches",
                c => new
                    {
                        ExternalId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Country = c.String(),
                        League = c.String(),
                        Season = c.String(),
                        Stage = c.String(),
                        Player_ExternalId = c.Int(),
                        AwayTeam_ExternalId = c.Int(),
                        HomeTeam_ExternalId = c.Int(),
                    })
                .PrimaryKey(t => t.ExternalId)
                .ForeignKey("ENETSCORES.Players", t => t.Player_ExternalId)
                .ForeignKey("ENETSCORES.Teams", t => t.AwayTeam_ExternalId)
                .ForeignKey("ENETSCORES.Teams", t => t.HomeTeam_ExternalId)
                .Index(t => t.Player_ExternalId)
                .Index(t => t.AwayTeam_ExternalId)
                .Index(t => t.HomeTeam_ExternalId);
            
            CreateTable(
                "ENETSCORES.Players",
                c => new
                    {
                        ExternalId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Match_ExternalId = c.Int(),
                        Match_ExternalId1 = c.Int(),
                    })
                .PrimaryKey(t => t.ExternalId)
                .ForeignKey("ENETSCORES.Matches", t => t.Match_ExternalId)
                .ForeignKey("ENETSCORES.Matches", t => t.Match_ExternalId1)
                .Index(t => t.Match_ExternalId)
                .Index(t => t.Match_ExternalId1);
            
            CreateTable(
                "ENETSCORES.Teams",
                c => new
                    {
                        ExternalId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Acronym = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "ENETSCORES.Possessions",
                c => new
                    {
                        ExternalId = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        Minute = c.String(),
                        HomePossession = c.String(),
                        AwayPossession = c.String(),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "ENETSCORES.ShotOffs",
                c => new
                    {
                        ExternalId = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "ENETSCORES.ShotOns",
                c => new
                    {
                        ExternalId = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ENETSCORES.Matches", "HomeTeam_ExternalId", "ENETSCORES.Teams");
            DropForeignKey("ENETSCORES.Players", "Match_ExternalId1", "ENETSCORES.Matches");
            DropForeignKey("ENETSCORES.Matches", "AwayTeam_ExternalId", "ENETSCORES.Teams");
            DropForeignKey("ENETSCORES.Players", "Match_ExternalId", "ENETSCORES.Matches");
            DropForeignKey("ENETSCORES.Matches", "Player_ExternalId", "ENETSCORES.Players");
            DropIndex("ENETSCORES.Players", new[] { "Match_ExternalId1" });
            DropIndex("ENETSCORES.Players", new[] { "Match_ExternalId" });
            DropIndex("ENETSCORES.Matches", new[] { "HomeTeam_ExternalId" });
            DropIndex("ENETSCORES.Matches", new[] { "AwayTeam_ExternalId" });
            DropIndex("ENETSCORES.Matches", new[] { "Player_ExternalId" });
            DropTable("ENETSCORES.ShotOns");
            DropTable("ENETSCORES.ShotOffs");
            DropTable("ENETSCORES.Possessions");
            DropTable("ENETSCORES.Teams");
            DropTable("ENETSCORES.Players");
            DropTable("ENETSCORES.Matches");
            DropTable("ENETSCORES.Goals");
            DropTable("ENETSCORES.Crosses");
            DropTable("ENETSCORES.Corners");
        }
    }
}
