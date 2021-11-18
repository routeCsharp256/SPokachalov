using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(9)]
    public class MerchItemTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@" CREATE TABLE IF NOT EXISTS merch_items
            (
                Id bigserial NOT NULL,
                MerchPackId bigint NOT NULL DEFAULT 0,
                CustomerId bigint NOT NULL DEFAULT 0,
                StatusId bigint NOT NULL DEFAULT 0,
                IssueTypeid bigint NOT NULL DEFAULT 0,
                OrderDate timestamp without time zone NOT NULL DEFAULT (now())::timestamp without time zone,
                ConfirmDate timestamp without time zone,
                Description text,
                CONSTRAINT PK_MerchItems PRIMARY KEY (Id),
                CONSTRAINT FK_CustomerId_to_customers FOREIGN KEY (CustomerId)
                        REFERENCES customers (Id) MATCH SIMPLE,
                CONSTRAINT FK_IssueTypeid_to_issue_types FOREIGN KEY (IssueTypeid)
                        REFERENCES issue_types (Id) MATCH SIMPLE,
                CONSTRAINT FK_MerchPackId_to_merch_pack FOREIGN KEY (MerchPackId)
                        REFERENCES merch_packs (Id) MATCH SIMPLE,
                CONSTRAINT FK_MerchStatusId_to_merch_status FOREIGN KEY (StatusId)
                        REFERENCES merch_status (Id) MATCH SIMPLE
            )");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS merch_items;");
        }
    }
}