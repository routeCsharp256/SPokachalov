using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(7)]
    public class MerchPackItemsSkusTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE TABLE IF NOT EXISTS merch_pack_items_skus
            (
                Id bigserial NOT NULL,
                MerchPackItemId bigint NOT NULL DEFAULT 0,
                SkuId bigint NOT NULL DEFAULT 0,
                CONSTRAINT PK_merch_pack_tems_skus PRIMARY KEY (Id),
                CONSTRAINT FK_MerchPackItemId_to_merch_pack_tems_Id FOREIGN KEY (MerchPackItemId)
                    REFERENCES merch_pack_items (Id) MATCH SIMPLE,
                CONSTRAINT FK_SkuId_to_skus_Id FOREIGN KEY (SkuId)
                    REFERENCES skus (Id) MATCH SIMPLE
            )");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS merch_pack_items_skus;");
        }
    }
}