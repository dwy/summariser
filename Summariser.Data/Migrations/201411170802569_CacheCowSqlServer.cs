namespace Summariser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CacheCowSqlServer : DbMigration
    {
        public override void Up()
        {
			Sql(@"CREATE TABLE [dbo].[CacheState](
				[CacheKeyHash] [binary](20) NOT NULL,
				[RoutePattern] [nvarchar](256) NOT NULL,
				[ResourceUri] [nvarchar](256) NOT NULL,
				[ETag] [nvarchar](100) NOT NULL,
				[LastModified] [datetime] NOT NULL,
			 CONSTRAINT [PK_CacheState] PRIMARY KEY CLUSTERED 
			(
				[CacheKeyHash] ASC
			)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
			)");

			CreateStoredProcedure(
				"dbo.Server_GetCache",
				p => new { cacheKeyHash = p.Binary(20), },
				body: @"SET NOCOUNT ON
					SELECT 
						ETag, LastModified
					FROM
						dbo.CacheState
					WHERE
						CacheKeyHash = @cacheKeyHash"
				);

			CreateStoredProcedure(
				"dbo.Server_AddUpdateCache",
				p => new
				{
					cacheKeyHash = p.Binary(20),
					routePattern = p.String(256, false, true),
					resourceUri = p.String(256, false, true),
					eTag = p.String(100, false, true),
					lastModified = p.DateTime()
				},
				body:
					@"-- SET NOCOUNT ON added to prevent extra result sets from
						-- interfering with SELECT statements.
						SET NOCOUNT ON

						BEGIN TRAN
						IF EXISTS (SELECT 1 FROM dbo.CacheState 
								WITH (UPDLOCK,SERIALIZABLE) WHERE CacheKeyHash = @cacheKeyHash)
							BEGIN
								UPDATE dbo.CacheState SET 
										ETag = @eTag,
										LastModified = @lastModified,
										RoutePattern = @routePattern,
										ResourceUri	 = @resourceUri
									WHERE CacheKeyHash = @cacheKeyHash
							END
						ELSE
							BEGIN
								INSERT INTO dbo.CacheState 
									(CacheKeyHash, RoutePattern, ResourceUri, ETag, LastModified)
								values 
									(@cacheKeyHash, @routePattern, @resourceUri, @eTag, @lastModified)
							END
						COMMIT TRAN"
				);

			CreateStoredProcedure(
				"dbo.Server_ClearCache",
				p => new { },
				body:
					@"SET NOCOUNT OFF
					DELETE FROM [dbo].[CacheState]"
				);

			CreateStoredProcedure(
				"dbo.Server_DeleteCacheById",
				p => new
				{
					CacheKeyHash = p.Binary(20),
				},
				body:
					@"SET NOCOUNT OFF
					DELETE FROM dbo.CacheState WHERE CacheKeyHash = @CacheKeyHash"
				);

			CreateStoredProcedure(
				"dbo.Server_DeleteCacheByResourceUri",
				p => new
				{
					resourceUri = p.String(256, false, true),
				},
				body:
					@"SET NOCOUNT OFF
					DELETE FROM dbo.CacheState WHERE ResourceUri = @resourceUri"
				);

			CreateStoredProcedure(
				"dbo.Server_DeleteCacheByRoutePattern",
				p => new
				{
					routePattern = p.String(256, false, true),
				},
				body:
					@"SET NOCOUNT OFF
					DELETE FROM dbo.CacheState WHERE RoutePattern = @routePattern"
				);
        }
        
        public override void Down()
        {
			DropTable("dbo.CacheState");
			DropStoredProcedure("dbo.Server_GetCache");
			DropStoredProcedure("dbo.Server_AddUpdateCache");
			DropStoredProcedure("dbo.Server_ClearCache");
			DropStoredProcedure("dbo.Server_DeleteCacheById");
			DropStoredProcedure("dbo.Server_DeleteCacheByResourceUri");
			DropStoredProcedure("dbo.Server_DeleteCacheByRoutePattern");
        }
    }
}
