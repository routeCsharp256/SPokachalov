using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(11, TransactionBehavior.None)]
    public class AddIndexes: ForwardOnlyMigration 
    {
        public override void Up()
        {
            Execute.Sql(@"
               -- CREATE INDEX CONCURRENTLY stocks_sku_id_idx ON stocks (sku_id)"
            );
        }
    }
}