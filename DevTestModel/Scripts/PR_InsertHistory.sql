USE TestDevALL;
---- procedure insertinto WORKING 
BEGIN TRAN
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DataHistoryArticle')
BEGIN
    CREATE TABLE DataHistoryArticle
    (
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[Id] [nvarchar](max) NOT NULL,
	[Guid] [nvarchar](max) NULL,
	[PublishedOn] [bigint] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Tags] [nvarchar](max) NULL,
	[Lang] [nvarchar](max) NULL,
	[Upvotes] [nvarchar](max) NULL,
	[Downvotes] [nvarchar](max) NULL,
	[Categories] [nvarchar](max) NULL,
	[SourceInfoId] [int] NULL,
	[NewsApiResponseApiResponseId] [int] NULL,
	Active bit NULL DEFAULT(1), 
	DateAndTimeInserted smalldatetime NULL
    );
END
GO

IF OBJECT_ID('PR_InsertHistory', 'P') IS NOT NULL
    DROP PROCEDURE PR_InsertHistory;
GO
CREATE PROCEDURE PR_InsertHistory
AS
BEGIN
    SET NOCOUNT ON;

-- Use the MERGE statement to insert or update records in DataHistoryArticle
MERGE INTO dbo.DataHistoryArticle AS target
USING dbo.NewsArticle AS source
ON target.Id = source.Id 
WHEN MATCHED THEN
    UPDATE
    SET
        target.Guid = source.Guid,
        target.PublishedOn = source.PublishedOn,
        target.ImageUrl = source.ImageUrl,
        target.Title = source.Title,
        target.Url = source.Url,
        target.Body = source.Body,
        target.Tags = source.Tags,
        target.Lang = source.Lang,
        target.Upvotes = source.Upvotes,
        target.Downvotes = source.Downvotes,
        target.Categories = source.Categories,
        target.SourceInfoId = source.SourceInfoId,
        target.NewsApiResponseApiResponseId = source.NewsApiResponseApiResponseId,
        target.Active = 1, -- Assuming Active should be set to 1
        target.DateAndTimeInserted = GETDATE()
WHEN NOT MATCHED THEN
    INSERT (
        Id,
        Guid,
        PublishedOn,
        ImageUrl,
        Title,
        Url,
        Body,
        Tags,
        Lang,
        Upvotes,
        Downvotes,
        Categories,
        SourceInfoId,
        NewsApiResponseApiResponseId,
        Active,
        DateAndTimeInserted
    )
    VALUES (
        source.Id,
        source.Guid,
        source.PublishedOn,
        source.ImageUrl,
        source.Title,
        source.Url,
        source.Body,
        source.Tags,
        source.Lang,
        source.Upvotes,
        source.Downvotes,
        source.Categories,
        source.SourceInfoId,
        source.NewsApiResponseApiResponseId,
        1, -- Assuming Active should be set to 1
        GETDATE()
    );

UPDATE dbo.NewsArticle 
SET 
	SourceInfoId = SourceInfoModel.SourceId
FROM 
	dbo.NewsArticle
INNER JOIN 
	dbo.SourceInfoModel 
ON 
	SourceInfoModel.ID = NewsArticle.ID;

--DBCC CHECKIDENT('dbo.NewsArticle',RESEED,0);
--DBCC CHECKIDENT('dbo.DataHistoryArticle',RESEED,0);
PRINT 'END'
END;
GO

--DBCC CHECKIDENT('dbo.DataHistoryArticle',RESEED,0);
--EXEC PR_InsertHistory

GO


SELECT COUNT(*) as 'source' FROM dbo.SourceInfoModel
SELECT * FROM dbo.SourceInfoModel
SELECT COUNT(*) as 'news' FROM dbo.NewsArticle
SELECT * FROM dbo.NewsArticle
SELECT COUNT(*) as 'history' FROM dbo.DataHistoryArticle
SELECT * FROM dbo.DataHistoryArticle

ROLLBACK TRAN; -- This can be changed to COMMIT TRAN; if you want to commit the changes

/*


-- Display the current state of DataHistoryArticle before the operation
--SELECT 'DataHistoryArticle Before Operation' AS 'INFO', * 
--FROM dbo.DataHistoryArticle;
--DBCC CHECKIDENT('dbo.DataHistoryArticle',RESEED,0);
--UPDATE dbo.NewsArticle SET NewsApiResponseApiResponseId

--SELECT * FROM dbo.DataHistoryArticle where Id = 22425506
-- Display the updated state of DataHistoryArticle after the operation
--SELECT 
--	--'DataHistoryArticle After Operation' AS 'INFO', 
--	* 
--FROM dbo.DataHistoryArticle
--ORDER BY
--	ArticleId ASC;
--DISABLE TRIGGER AfterInsert_NewsArticle ON NewsArticle;
--GO

*/