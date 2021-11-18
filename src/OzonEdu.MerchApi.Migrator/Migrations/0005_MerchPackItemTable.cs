using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(5)]
    public class MerchPackItemTable: Migration
    {
        public override void Up()
        {
       Execute.Sql(@"CREATE TABLE IF NOT EXISTS merch_pack_items
            (
                Id bigserial NOT NULL,
                Name integer NOT NULL DEFAULT 0,
                CONSTRAINT PK_merch_pack_tems PRIMARY KEY(Id)
            );");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS merch_pack_items;");
        }
    }
}