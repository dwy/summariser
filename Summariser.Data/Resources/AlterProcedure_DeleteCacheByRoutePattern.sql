-- =============================================
-- Author:		Ali Kheyrollahi
-- Create date: 2012-07-12
-- Description:	Deletes all CacheKey records by its route pattern
-- =============================================
CREATE PROCEDURE Server_DeleteCacheByRoutePattern
	@routePattern NVARCHAR(256) 
AS
BEGIN
	SET NOCOUNT OFF

	DELETE FROM dbo.CacheState WHERE RoutePattern = @routePattern
END