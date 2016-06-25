-- Creating table 'Path_Task'
CREATE TABLE [dbo].[Path_Task] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TaskId] int  NOT NULL,
    [PathId] int  NOT NULL,
    [Position] int  NOT NULL,
);
GO
-- Creating foreign key on [Task_Id] in table 'Path_Task'
ALTER TABLE [dbo].[Path_Task]
ADD CONSTRAINT [FK_Path_TaskTask]
    FOREIGN KEY ([TaskId])
    REFERENCES [dbo].[Tasks]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO
-- Creating foreign key on [Path_Id] in table 'Path_Task'
ALTER TABLE [dbo].[Path_Task]
ADD CONSTRAINT [FK_Path_TaskPath]
    FOREIGN KEY ([PathId])
    REFERENCES [dbo].[Pathes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO
-- Creating primary key on [Id] in table 'Path_Task'
ALTER TABLE [dbo].[Path_Task]
ADD CONSTRAINT [PK_Path_Task]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_Path_TaskTask'
CREATE INDEX [IX_FK_Path_TaskTask]
ON [dbo].[Path_Task]
    ([TaskId]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_Path_TaskPath'
CREATE INDEX [IX_FK_Path_TaskPath]
ON [dbo].[Path_Task]
    ([PathId]);