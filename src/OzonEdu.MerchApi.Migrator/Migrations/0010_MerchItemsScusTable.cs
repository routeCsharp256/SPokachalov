using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(10)]
    public class MerchItemsScusTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE TABLE IF NOT EXISTS  merch_items_skus
            (
                Id bigserial NOT NULL,
                MerchItemId bigint NOT NULL DEFAULT 0,
                SkuId bigint NOT NULL DEFAULT 0,
                CONSTRAINT PK_merch_items_skus PRIMARY KEY (Id),
                CONSTRAINT FK_MerchItemId_to_merch_items FOREIGN KEY (MerchItemId)
                    REFERENCES merch_items (Id) MATCH SIMPLE,
                CONSTRAINT FK_SkuId_to_skus FOREIGN KEY (SkuId)
                    REFERENCES skus (Id) MATCH SIMPLE
            )");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS merch_items_skus;");
        }
    }
}