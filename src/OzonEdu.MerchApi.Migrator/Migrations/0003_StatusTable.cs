using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(3)]
    public class MerchStatusTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE TABLE IF NOT EXISTS merch_status
            (
                Id bigserial NOT NULL,
                Name text,
                CONSTRAINT PK_merch_status PRIMARY KEY (Id)
            )");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE  IF EXISTS merch_status;");
        }
    }
}