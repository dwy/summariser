-- =============================================
-- Author:		Ali Kheyrollahi
-- Create date: 2012-07-12
-- Description:	Deletes a CacheKey record by its id
-- =============================================
CREATE PROCEDURE Server_DeleteCacheById
	@CacheKeyHash BINARY(20) 
AS
BEGIN
	SET NOCOUNT OFF
	DELETE FROM dbo.CacheState WHERE CacheKeyHash = @CacheKeyHash
END