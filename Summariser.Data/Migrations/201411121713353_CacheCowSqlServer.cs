namespace Summariser.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class CacheCowSqlServer : DbMigration
    {
        public override void Up()
        {
            // NOTE since CREATE/ALTER PROCEDURE needs to be the only statement in a batch, we create dummy procedures using
            // exec('CREATE PROCEDURE [dbo].[Server_AddUpdateCache] AS BEGIN SET NOCOUNT ON; END') and then execute ALTER for each separately.
            Sql(Data.Resources.CacheCow_CreateDatabaseAndProcedures);

            Sql(Data.Resources.AlterProcedure_AddOrUpdateCache);
            Sql(Data.Resources.AlterProcedure_ClearCache);
            Sql(Data.Resources.AlterProcedure_DeleteCacheById);
            Sql(Data.Resources.AlterProcedure_DeleteCacheByResourceUri);
            Sql(Data.Resources.AlterProcedure_DeleteCacheByRoutePattern);
            Sql(Data.Resources.AlterProcedure_GetCache);
        }
        
        public override void Down()
        {
        }
    }
}
