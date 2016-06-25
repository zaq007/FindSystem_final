-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserProfiles'
CREATE TABLE [dbo].[UserProfiles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [TeamName] nvarchar(max)  NOT NULL DEFAULT 'NoName',
    [StartTime] datetime  NULL,
    [EndTime] datetime  NULL
);
GO
-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserProfiles'
ALTER TABLE [dbo].[UserProfiles]
ADD CONSTRAINT [PK_UserProfiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

CREATE TRIGGER [dbo].[Trigger_UserProfiles]
    ON [dbo].[UserProfiles]
    FOR INSERT
    AS
    BEGIN
        SET NoCount ON
		INSERT INTO [dbo].[States] ([UserId]) SELECT Id FROM inserted
    END