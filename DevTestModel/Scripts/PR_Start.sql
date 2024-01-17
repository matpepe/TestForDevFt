USE TestDevALL;
---- procedure insertinto WORKING 
BEGIN TRANSACTION

IF OBJECT_ID('PR_InsertHistory', 'P') IS NOT NULL
    DROP PROCEDURE PR_InsertHistory;
GO
CREATE PROCEDURE PR_InsertHistory
AS
BEGIN
    SET NOCOUNT ON;

	MERGE INTO 
		dbo.DataHistoryArticle AS target
	USING 
		dbo.NewsArticle AS source
		ON target.Id = source.Id 
	WHEN MATCHED 
	THEN
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
        target.Active = 1, 
        target.DateAndTimeInserted = GETDATE()
		WHEN 
			NOT MATCHED 
			THEN
	INSERT 
		(
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
	VALUES 
		(
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
			1, 
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

IF NOT EXISTS 
	(SELECT 1 FROM dbo.DC_NewsCategoryCR)
BEGIN
	DBCC CHECKIDENT('dbo.DC_NewsCategoryCR',RESEED,0);

    INSERT INTO 
		dbo.DC_NewsCategoryCR(CategoryNewsName)
    SELECT 
	DISTINCT 
		Categories
    FROM 
		dbo.NewsArticle;
END;

DBCC CHECKIDENT('dbo.DC_NewsCategoryCR',RESEED,0);
PRINT 'END'
END;

GO
EXEC PR_InsertHistory ---- (execute by IActionMethod inApp)
GO

SET IDENTITY_INSERT NewsApiResponse ON;

IF NOT EXISTS (SELECT 1 FROM dbo.NewsApiResponse)
BEGIN

	INSERT INTO dbo.NewsApiResponse(ApiResponseId,Type,Message,Id,NewsArticleArticleId)
	VALUES(1,200,N'https://min-api.cryptocompare.com/data/v2/news/?api_key=',1,NULL)

END;

GO
SET IDENTITY_INSERT NewsApiResponse OFF;
GO

SELECT COUNT(*) as 'infoCOunt','source' FROM dbo.SourceInfoModel
UNION ALL		
SELECT COUNT(*) as 'infoCOunt','news' FROM dbo.NewsArticle
UNION ALL		
SELECT COUNT(*) as 'infoCOunt','history' FROM dbo.DataHistoryArticle
UNION ALL		
SELECT COUNT(*) as 'infoCOunt','_NewsCategoryCR' FROM dbo.DC_NewsCategoryCR
UNION ALL		
SELECT COUNT(*) as 'infoCOunt','NewsApiResponse' FROM dbo.NewsApiResponse

--ROLLBACK TRAN; 

COMMIT TRAN

/*
##########################################################
--IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DataHistoryArticle')
--BEGIN
-- CREATE TABLE DataHistoryArticle
--    (
--	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
--	[Id] [nvarchar](max) NOT NULL,
--	[Guid] [nvarchar](max) NULL,
--	[PublishedOn] [bigint] NOT NULL,
--	[ImageUrl] [nvarchar](max) NULL,
--	[Title] [nvarchar](max) NULL,
--	[Url] [nvarchar](max) NULL,
--	[Body] [nvarchar](max) NULL,
--	[Tags] [nvarchar](max) NULL,
--	[Lang] [nvarchar](max) NULL,
--	[Upvotes] [nvarchar](max) NULL,
--	[Downvotes] [nvarchar](max) NULL,
--	[Categories] [nvarchar](max) NULL,
--	[SourceInfoId] [int] NULL,
--	[NewsApiResponseApiResponseId] [int] NULL,
--	Active bit NULL DEFAULT(1), 
--	DateAndTimeInserted smalldatetime NULL
--    );
--END
##########################################################
--SELECT COUNT(*) as 'source' FROM dbo.SourceInfoModel
--SELECT * FROM dbo.SourceInfoModel
--SELECT COUNT(*) as 'news' FROM dbo.NewsArticle
--SELECT * FROM dbo.NewsArticle
--SELECT COUNT(*) as 'history' FROM dbo.DataHistoryArticle
--SELECT * FROM dbo.DataHistoryArticle
--SELECT * FROM dbo.DC_NewsCategoryCR
--SELECT * FROM dbo.NewsApiResponse
##########################################################
--DROP PROCEDURE PR_InsertHistory
--DBCC CHECKIDENT('dbo.DataHistoryArticle',RESEED,0);
--DBCC CHECKIDENT('dbo.DataHistoryArticle',RESEED,0);
-- Display the current state of DataHistoryArticle before the operation
--SELECT 'DataHistoryArticle Before Operation' AS 'INFO', * 
--FROM dbo.DataHistoryArticle;
--DBCC CHECKIDENT('dbo.DataHistoryArticle',RESEED,0);
--UPDATE dbo.NewsArticle SET NewsApiResponseApiResponseId
--SELECT * FROM dbo.DataHistoryArticle where Id = 22425506
-- Display the updated state of DataHistoryArticle after the operation
--SELECT 
--'DataHistoryArticle After Operation' AS 'INFO', 
--FROM dbo.DataHistoryArticle
--ORDER BY
--	ArticleId ASC;
--DISABLE TRIGGER AfterInsert_NewsArticle ON NewsArticle;
##########################################################

*/
