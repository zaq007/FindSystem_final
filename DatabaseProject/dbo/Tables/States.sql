-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [UserId] int NOT NULL,
    [PathId] int  NULL,
    [Position] int  NOT NULL DEFAULT 0,
    [IsFrozen] bit  NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_States] PRIMARY KEY ([UserId]),
);
GO
-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [FK_StateUserProfile]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserProfiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [Path_Id] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [FK_StatePath]
    FOREIGN KEY ([PathId])
    REFERENCES [dbo].[Pathes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [UserId] in table 'States'

GO
-- Creating non-clustered index for FOREIGN KEY 'FK_StatePath'
CREATE INDEX [IX_FK_StatePath]
ON [dbo].[States]
    ([PathId]);