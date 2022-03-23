﻿USE [TabloidCLI]
GO

ALTER TABLE PostTag
DROP CONSTRAINT "FK_PostTag_Post";
ALTER TABLE PostTag
ADD CONSTRAINT "FK_PostTag_Post"
FOREIGN KEY (PostId)
REFERENCES Post(Id)
ON DELETE CASCADE;

ALTER TABLE Note
DROP CONSTRAINT "FK_Note_Posti";
ALTER TABLE Note
ADD CONSTRAINT "FK_Note_Posti"
FOREIGN KEY (PostId)
REFERENCES Post(Id)
ON DELETE CASCADE;