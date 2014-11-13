-- =============================================
-- Author:		Ali Kheyrollahi
-- Create date: 2012-07-12
-- Description:	returns cache entry by its Id
-- =============================================
CREATE PROCEDURE Server_GetCache
	@cacheKeyHash		BINARY(20)
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		ETag, LastModified
	FROM
		dbo.CacheState
	WHERE
		CacheKeyHash = @cacheKeyHash

END