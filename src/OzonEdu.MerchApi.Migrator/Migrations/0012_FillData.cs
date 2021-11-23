using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Temp
{
    [Migration(12)]
    public class FillData : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@" INSERT INTO merch_status(id, name) VALUES (1, 'Done'), (2, 'Wait');");
            
            Execute.Sql(@" INSERT INTO issue_types(id, name) VALUES (1, 'Auto'), (2, 'Manual');");
        }
    }
}