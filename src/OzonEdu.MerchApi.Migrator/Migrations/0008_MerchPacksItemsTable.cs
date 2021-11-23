using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(8)]
    public class MerchPacksItemsTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE TABLE IF NOT EXISTS merch_packs_items
            (
                Id bigserial NOT NULL,
                MerchPackId bigint NOT NULL DEFAULT 0,
                MerchPackItemId bigint NOT NULL DEFAULT 0,
                CONSTRAINT PK_merch_packs_items PRIMARY KEY(Id),
                 CONSTRAINT FK_MerchPackId_to_merch_pack_Id FOREIGN KEY (MerchPackId)
                    REFERENCES merch_packs (Id) MATCH SIMPLE,
                CONSTRAINT FK_MerchPackItemId_to_merch_pack_items_Id FOREIGN KEY (MerchPackItemId)
                    REFERENCES merch_pack_items (Id) MATCH SIMPLE
            );");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS merch_packs_items;");
        }
    }
}