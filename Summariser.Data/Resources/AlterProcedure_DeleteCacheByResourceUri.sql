-- =============================================
-- Author:		Ali Kheyrollahi
-- Create date: 2013-11-16
-- Description:	Deletes all CacheKey records by its resource uri
-- =============================================
ALTER PROCEDURE Server_DeleteCacheByResourceUri
	@resourceUri NVARCHAR(256) 
AS
BEGIN
	SET NOCOUNT OFF

	DELETE FROM dbo.CacheState WHERE ResourceUri = @resourceUri
END