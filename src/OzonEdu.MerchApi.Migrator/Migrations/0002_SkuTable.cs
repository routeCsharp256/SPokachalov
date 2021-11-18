using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(2)]
    public class SkuTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE TABLE IF NOT EXISTS skus
            (
                Id bigserial NOT NULL,
                Name text,
                Description text,
                StokItemParams text,
                CONSTRAINT PK_skus PRIMARY KEY(Id)
            );");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS skus;");
        }
    }
}