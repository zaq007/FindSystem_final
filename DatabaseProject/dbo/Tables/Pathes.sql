-- Creating table 'Pathes'
CREATE TABLE [dbo].[Pathes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO
-- Creating primary key on [Id] in table 'Pathes'
ALTER TABLE [dbo].[Pathes]
ADD CONSTRAINT [PK_Pathes]
    PRIMARY KEY CLUSTERED ([Id] ASC);