namespace MatchXMLParser.Repos
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "DCS.Corners",
                c => new
                    {
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "DCS.Crosses",
                c => new
                    {
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "DCS.Goals",
                c => new
                    {
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        ScorerId = c.String(),
                        AssistId = c.String(),
                        Type = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "DCS.Matches",
                c => new
                    {
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Date = c.DateTime(nullable: false),
                        Country = c.String(),
                        League = c.String(),
                        Season = c.String(),
                        Stage = c.String(),
                        Player_ExternalId = c.Decimal(precision: 10, scale: 0),
                        AwayTeam_ExternalId = c.Decimal(precision: 10, scale: 0),
                        HomeTeam_ExternalId = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ExternalId)
                .ForeignKey("DCS.Players", t => t.Player_ExternalId)
                .ForeignKey("DCS.Teams", t => t.AwayTeam_ExternalId)
                .ForeignKey("DCS.Teams", t => t.HomeTeam_ExternalId)
                .Index(t => t.Player_ExternalId)
                .Index(t => t.AwayTeam_ExternalId)
                .Index(t => t.HomeTeam_ExternalId);
            
            CreateTable(
                "DCS.Players",
                c => new
                    {
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(),
                        Match_ExternalId = c.Decimal(precision: 10, scale: 0),
                        Match_ExternalId1 = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ExternalId)
                .ForeignKey("DCS.Matches", t => t.Match_ExternalId)
                .ForeignKey("DCS.Matches", t => t.Match_ExternalId1)
                .Index(t => t.Match_ExternalId)
                .Index(t => t.Match_ExternalId1);
            
            CreateTable(
                "DCS.Teams",
                c => new
                    {
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        FullName = c.String(),
                        Acronym = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "DCS.Possessions",
                c => new
                    {
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        HomePossession = c.String(),
                        AwayPossession = c.String(),
                        TeamId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "DCS.ShotOffs",
                c => new
                    {
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
            CreateTable(
                "DCS.ShotOns",
                c => new
                    {
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.ExternalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("DCS.Matches", "HomeTeam_ExternalId", "DCS.Teams");
            DropForeignKey("DCS.Players", "Match_ExternalId1", "DCS.Matches");
            DropForeignKey("DCS.Matches", "AwayTeam_ExternalId", "DCS.Teams");
            DropForeignKey("DCS.Players", "Match_ExternalId", "DCS.Matches");
            DropForeignKey("DCS.Matches", "Player_ExternalId", "DCS.Players");
            DropIndex("DCS.Players", new[] { "Match_ExternalId1" });
            DropIndex("DCS.Players", new[] { "Match_ExternalId" });
            DropIndex("DCS.Matches", new[] { "HomeTeam_ExternalId" });
            DropIndex("DCS.Matches", new[] { "AwayTeam_ExternalId" });
            DropIndex("DCS.Matches", new[] { "Player_ExternalId" });
            DropTable("DCS.ShotOns");
            DropTable("DCS.ShotOffs");
            DropTable("DCS.Possessions");
            DropTable("DCS.Teams");
            DropTable("DCS.Players");
            DropTable("DCS.Matches");
            DropTable("DCS.Goals");
            DropTable("DCS.Crosses");
            DropTable("DCS.Corners");
        }
    }
}
