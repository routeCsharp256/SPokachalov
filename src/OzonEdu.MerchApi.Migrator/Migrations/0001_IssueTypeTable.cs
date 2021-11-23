using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(1)]
    public class IssueTypeTable : Migration 
    {
        public override void Up()
        { 
            Execute.Sql(@"CREATE TABLE IF NOT EXISTS issue_types(
                         Id bigserial NOT NULL,
                         Name text NOT NULL,
                         CONSTRAINT PK_issue_types PRIMARY KEY (Id));");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS issue_types;");
        }
    }
}