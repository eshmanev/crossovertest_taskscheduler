CREATE TABLE [dbo].[Client](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](15) NOT NULL
)
GO

INSERT INTO [dbo].[Client]([Name], [Address])	VALUES('Client1', 'localhost')
INSERT INTO [dbo].[Client]([Name], [Address])	VALUES('Client2', 'localhost')
INSERT INTO [dbo].[Client]([Name], [Address])	VALUES('Client3', 'localhost')
INSERT INTO [dbo].[Client]([Name], [Address])	VALUES('Client4', 'localhost')
INSERT INTO [dbo].[Client]([Name], [Address])	VALUES('Client5', 'localhost')
INSERT INTO [dbo].[Client]([Name], [Address])	VALUES('Client6', 'localhost')
INSERT INTO [dbo].[Client]([Name], [Address])	VALUES('Client7', 'localhost')
INSERT INTO [dbo].[Client]([Name], [Address])	VALUES('Client8', 'localhost')
INSERT INTO [dbo].[Client]([Name], [Address])	VALUES('Client9', 'localhost')
INSERT INTO [dbo].[Client]([Name], [Address])	VALUES('Client10', 'localhost')

CREATE TABLE [dbo].[Command](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[CommandText] [nvarchar](50) NOT NULL,
	[Parameters] [nvarchar](255) NULL,
	CONSTRAINT [FK_Command_Client] FOREIGN KEY([ClientId]) REFERENCES [dbo].[Client] ([Id])
)
GO
INSERT INTO [dbo].[Command]([ClientId], [CommandText], [Parameters]) VALUES ('1', 'echo', 'Hello, Client1!')
INSERT INTO [dbo].[Command]([ClientId], [CommandText], [Parameters]) VALUES ('2', 'echo', 'Hello, Client2!')
INSERT INTO [dbo].[Command]([ClientId], [CommandText], [Parameters]) VALUES ('3', 'echo', 'Hello, Client3!')
INSERT INTO [dbo].[Command]([ClientId], [CommandText], [Parameters]) VALUES ('4', 'echo', 'Hello, Client4!')
INSERT INTO [dbo].[Command]([ClientId], [CommandText], [Parameters]) VALUES ('5', 'echo', 'Hello, Client5!')
INSERT INTO [dbo].[Command]([ClientId], [CommandText], [Parameters]) VALUES ('6', 'md', 'C:\MyTempFolder')
INSERT INTO [dbo].[Command]([ClientId], [CommandText], [Parameters]) VALUES ('6', 'rd', 'C:\MyTempFolder')

CREATE TABLE [dbo].[ScheduledCommand](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[CommandId] [int] NOT NULL,
	[NextTime] [datetime] NOT NULL,
	[TriggerType] [int] NOT NULL,
	CONSTRAINT [FK_ScheduledCommand_Command] FOREIGN KEY([CommandId]) REFERENCES [dbo].[Command] ([Id])
)
GO


CREATE TABLE [dbo].[Log](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Output] [nvarchar](max) NULL,
	[CommandId] [int] NOT NULL,
	 CONSTRAINT [FK_Log_Command] FOREIGN KEY([CommandId]) REFERENCES [dbo].[Command] ([Id])
)
GO
