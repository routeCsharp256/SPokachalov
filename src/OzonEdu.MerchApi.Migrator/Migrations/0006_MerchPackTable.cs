using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(6)]
    public class MerchPackTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE TABLE IF NOT EXISTS merch_packs(
                Id bigserial NOT NULL,
                Name text,
                CONSTRAINT PK_merch_packs PRIMARY KEY (Id)
            )");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS merch_packs;");
        }
    }
}