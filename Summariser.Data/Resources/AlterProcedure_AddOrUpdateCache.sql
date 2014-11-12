-- =============================================
-- Author:		Ali Kheyrollahi
-- Create date: 2012-07-12
-- Description:	Adds or updates cache entry
-- =============================================
ALTER PROCEDURE Server_AddUpdateCache
	@cacheKeyHash	BINARY(20),
	@routePattern	NVARCHAR(256),
	@resourceUri	NVARCHAR(256),
	@eTag			NVARCHAR(100),
	@lastModified	DATETIME
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
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
	COMMIT TRAN

END