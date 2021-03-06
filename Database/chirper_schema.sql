USE [ChirpDb]

CREATE SEQUENCE [seq_chirp] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 100
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
CREATE SEQUENCE [seq_chirp_user] 
 AS [int]
 START WITH 1
 INCREMENT BY 10
 MINVALUE -2147483648
 MAXVALUE 2147483647
 CACHE 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [chirp](
	[id] [bigint] NOT NULL,
	[user_id] [int] NOT NULL,
	[chirp_type_id] [tinyint] NOT NULL,
	[chirp_time_utc] [datetime] NOT NULL,
	[contents] [nvarchar](100) NOT NULL,
	[original_chirp_id] [bigint] NULL,
	[score] [int] NOT NULL,
 CONSTRAINT [pk_chirp] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [chirp_type](
	[id] [tinyint] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
 CONSTRAINT [pk_chirp_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uq_chirp_type_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [chirp_user](
	[id] [int] NOT NULL,
	[username] [nvarchar](255) NOT NULL,
	[username_normalized] [nvarchar](255) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[security_stamp] [nvarchar](255) NULL,
 CONSTRAINT [pk_chirp_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uq_chirp_user_username_normalized] UNIQUE NONCLUSTERED 
(
	[username_normalized] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [chirp_user_follower](
	[user_id] [int] NOT NULL,
	[follower_id] [int] NOT NULL,
 CONSTRAINT [pk_chirp_user_follower] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[follower_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [hashtag](
	[hashtag] [nvarchar](100) NOT NULL,
	[time_utc] [datetime] NOT NULL,
	[chirp_id] [bigint] NOT NULL,
 CONSTRAINT [pk_hashtag] PRIMARY KEY CLUSTERED 
(
	[hashtag] ASC,
	[time_utc] ASC,
	[chirp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [reaction](
	[chirp_id] [bigint] NOT NULL,
	[user_id] [int] NOT NULL,
	[reaction_type_id] [tinyint] NOT NULL,
 CONSTRAINT [pk_reaction] PRIMARY KEY CLUSTERED 
(
	[chirp_id] ASC,
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [reaction_type](
	[id] [tinyint] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[is_positive] [bit] NOT NULL,
	[is_negative] [bit] NOT NULL,
 CONSTRAINT [pk_reaction_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uq_reaction_type] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [timeline](
	[user_id] [int] NOT NULL,
	[time_utc] [datetime] NOT NULL,
	[chirp_id] [bigint] NOT NULL,
 CONSTRAINT [pk_timeline] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[time_utc] ASC,
	[chirp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [ix_chirp_original_chirp_id] ON [chirp]
(
	[original_chirp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [ix_chirp_user_id] ON [chirp]
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [ix_chirp_user_follower_follower_id] ON [chirp_user_follower]
(
	[follower_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [ix_hashtag_time_utc] ON [hashtag]
(
	[time_utc] ASC
)
INCLUDE ( 	[hashtag],
	[chirp_id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [ix_reaction_user_id] ON [reaction]
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [chirp]  WITH CHECK ADD  CONSTRAINT [fk_chirp_chirp_type_id] FOREIGN KEY([chirp_type_id])
REFERENCES [chirp_type] ([id])
GO
ALTER TABLE [chirp] CHECK CONSTRAINT [fk_chirp_chirp_type_id]
GO
ALTER TABLE [chirp]  WITH CHECK ADD  CONSTRAINT [fk_chirp_user_id] FOREIGN KEY([user_id])
REFERENCES [chirp_user] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [chirp] CHECK CONSTRAINT [fk_chirp_user_id]
GO
ALTER TABLE [chirp_user_follower]  WITH CHECK ADD  CONSTRAINT [fk_chirp_user_follower_follower_id] FOREIGN KEY([follower_id])
REFERENCES [chirp_user] ([id])
GO
ALTER TABLE [chirp_user_follower] CHECK CONSTRAINT [fk_chirp_user_follower_follower_id]
GO
ALTER TABLE [chirp_user_follower]  WITH CHECK ADD  CONSTRAINT [fk_chirp_user_follower_user_id] FOREIGN KEY([user_id])
REFERENCES [chirp_user] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [chirp_user_follower] CHECK CONSTRAINT [fk_chirp_user_follower_user_id]
GO
ALTER TABLE [hashtag]  WITH CHECK ADD  CONSTRAINT [fk_hashtag_chirp_id] FOREIGN KEY([chirp_id])
REFERENCES [chirp] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [hashtag] CHECK CONSTRAINT [fk_hashtag_chirp_id]
GO
ALTER TABLE [reaction]  WITH CHECK ADD  CONSTRAINT [fk_reaction_reaction_type_id] FOREIGN KEY([reaction_type_id])
REFERENCES [reaction_type] ([id])
GO
ALTER TABLE [reaction] CHECK CONSTRAINT [fk_reaction_reaction_type_id]
GO
ALTER TABLE [reaction]  WITH CHECK ADD  CONSTRAINT [fk_reaction_user_id] FOREIGN KEY([user_id])
REFERENCES [chirp_user] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [reaction] CHECK CONSTRAINT [fk_reaction_user_id]
GO
ALTER TABLE [timeline]  WITH CHECK ADD  CONSTRAINT [fk_timeline_user_id] FOREIGN KEY([user_id])
REFERENCES [chirp_user] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [timeline] CHECK CONSTRAINT [fk_timeline_user_id]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [populate_chirp_users] 
	-- Add the parameters for the stored procedure here
	@count INT = 1000
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @user_id INT;

    WHILE @count > 0
    BEGIN
        SET @user_id = NEXT VALUE FOR seq_chirp_user;

        INSERT INTO chirp_user (id, username, password)
        VALUES (@user_id, 'user_' + cast(@user_id AS NVARCHAR(10)), 'not_valid')

        SET @count = @count - 1
    END
END
GO

