using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(4)]
    public class CustomerTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE TABLE IF NOT EXISTS customers
            (
                Id bigserial NOT NULL,
                Mail text,
                Name text,
                MentorMail text,
                MentorName text,
                CONSTRAINT PK_customers PRIMARY KEY (Id)
            )");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS customers;");
        }
    }
}