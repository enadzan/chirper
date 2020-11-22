CREATE SCHEMA [ids]
GO

CREATE TABLE [ids].[device_code](
	[user_code] [nvarchar](200) NOT NULL,
	[client_id] [nvarchar](200) NOT NULL,
	[creation_time] [datetime2](7) NOT NULL,
	[data] [nvarchar](max) NOT NULL,
	[device_code] [nvarchar](200) NOT NULL,
	[expiration] [datetime2](7) NULL,
	[subject_id] [nvarchar](200) NULL,
  CONSTRAINT [pk_ids_device_code] PRIMARY KEY ([user_code]),
  CONSTRAINT [uq_ids_device_code_device_code] UNIQUE ([device_code])
)
GO

CREATE INDEX [ix_ids_device_code_expiration] ON [ids].[device_code] ([expiration])
GO

CREATE TABLE [ids].[persisted_grant](
	[grant_key] [nvarchar](200) NOT NULL,
	[grant_type] [nvarchar](50) NOT NULL,
	[client_id] [nvarchar](200) NOT NULL,
	[creation_time] [datetime2](7) NOT NULL,
	[data] [nvarchar](max) NOT NULL,
	[expiration] [datetime2](7) NULL,
	[subject_id] [nvarchar](200) NULL,
  CONSTRAINT [pk_ids_persistent_grant] PRIMARY KEY ([grant_key])
)
GO

CREATE INDEX [ix_ids_persistent_grant_expiration] ON [ids].[persisted_grant] ([expiration])
GO

CREATE INDEX [ix_ids_persistent_grant_subject_id] ON [ids].[persisted_grant] ([subject_id], [client_id], [grant_type])
GO
