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
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DCS.Crosses",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DCS.Goals",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        ScorerId = c.String(),
                        AssistId = c.String(),
                        Type = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DCS.Matches",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Date = c.DateTime(nullable: false),
                        Country = c.String(),
                        League = c.String(),
                        Season = c.String(),
                        Stage = c.String(),
                        Player_Id = c.Decimal(precision: 10, scale: 0),
                        AwayTeam_Id = c.Decimal(precision: 10, scale: 0),
                        HomeTeam_Id = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DCS.Players", t => t.Player_Id)
                .ForeignKey("DCS.Teams", t => t.AwayTeam_Id)
                .ForeignKey("DCS.Teams", t => t.HomeTeam_Id)
                .Index(t => t.Player_Id)
                .Index(t => t.AwayTeam_Id)
                .Index(t => t.HomeTeam_Id);
            
            CreateTable(
                "DCS.Players",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(),
                        Match_Id = c.Decimal(precision: 10, scale: 0),
                        Match_Id1 = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DCS.Matches", t => t.Match_Id)
                .ForeignKey("DCS.Matches", t => t.Match_Id1)
                .Index(t => t.Match_Id)
                .Index(t => t.Match_Id1);
            
            CreateTable(
                "DCS.Teams",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FullName = c.String(),
                        Acronym = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DCS.Possessions",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        HomePossession = c.String(),
                        AwayPossession = c.String(),
                        TeamId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DCS.ShotOffs",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DCS.ShotOns",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ExternalId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MatchId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Minute = c.String(),
                        Player1 = c.String(),
                        TeamId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("DCS.Matches", "HomeTeam_Id", "DCS.Teams");
            DropForeignKey("DCS.Players", "Match_Id1", "DCS.Matches");
            DropForeignKey("DCS.Matches", "AwayTeam_Id", "DCS.Teams");
            DropForeignKey("DCS.Players", "Match_Id", "DCS.Matches");
            DropForeignKey("DCS.Matches", "Player_Id", "DCS.Players");
            DropIndex("DCS.Players", new[] { "Match_Id1" });
            DropIndex("DCS.Players", new[] { "Match_Id" });
            DropIndex("DCS.Matches", new[] { "HomeTeam_Id" });
            DropIndex("DCS.Matches", new[] { "AwayTeam_Id" });
            DropIndex("DCS.Matches", new[] { "Player_Id" });
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
