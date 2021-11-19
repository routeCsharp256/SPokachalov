using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(11, TransactionBehavior.None)]
    public class AddIndexes: ForwardOnlyMigration 
    {
        public override void Up()
        {
            Execute.Sql(@"-- "
            );
        }
    }
}