
/****** Object:  Table [dbo].[CacheState]    Script Date: 07/25/2012 13:38:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CacheState]') AND type in (N'U'))
DROP TABLE [dbo].[CacheState]


/****** Object:  Table [dbo].[CacheState]    Script Date: 07/25/2012 13:38:35 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ANSI_PADDING ON

CREATE TABLE [dbo].[CacheState](
	[CacheKeyHash] [binary](20) NOT NULL,
	[RoutePattern] [nvarchar](256) NOT NULL,
	[ResourceUri] [nvarchar](256) NOT NULL,
	[ETag] [nvarchar](100) NOT NULL,
	[LastModified] [datetime] NOT NULL,
 CONSTRAINT [PK_CacheState] PRIMARY KEY CLUSTERED 
(
	[CacheKeyHash] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Server_AddUpdateCache]') AND type in (N'P', N'PC'))
	exec('CREATE PROCEDURE [dbo].[Server_AddUpdateCache] AS BEGIN SET NOCOUNT ON; END')

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Server_ClearCache]') AND type in (N'P', N'PC'))
	exec('CREATE PROCEDURE [dbo].[Server_ClearCache] AS BEGIN SET NOCOUNT ON; END')

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Server_DeleteCacheById]') AND type in (N'P', N'PC'))
	exec('CREATE PROCEDURE [dbo].[Server_DeleteCacheById] AS BEGIN SET NOCOUNT ON; END')

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Server_DeleteCacheByResourceUri]') AND type in (N'P', N'PC'))
	exec('CREATE PROCEDURE [dbo].[Server_DeleteCacheByResourceUri] AS BEGIN SET NOCOUNT ON; END')

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Server_DeleteCacheByRoutePattern]') AND type in (N'P', N'PC'))
	exec('CREATE PROCEDURE [dbo].[Server_DeleteCacheByRoutePattern] AS BEGIN SET NOCOUNT ON; END')

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Server_GetCache]') AND type in (N'P', N'PC'))
	exec('CREATE PROCEDURE [dbo].[Server_GetCache] AS BEGIN SET NOCOUNT ON; END')
