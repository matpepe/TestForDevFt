USE TestDevALL
-- WORKING SELECT 
BEGIN TRAN

--SELECT * FROM dbo.NewsArticle
--SELECT COUNT(*) as 'info' FROM dbo.NewsArticle

SELECT * FROM dbo.SourceInfoModel
SELECT COUNT(*) as 'source' FROM dbo.SourceInfoModel

--SELECT * FROM dbo.NewsApiResponse
--SELECT * FROM dbo.NewsArticle
SELECT COUNT(*) as 'news' FROM dbo.NewsArticle


--SELECT TOP 1 * FROM dbo.NewsArticle
--ORDER BY ArticleId desc 
--SELECT * FROM dbo.DataHistoryArticle
SELECT COUNT(*) as 'dataHistory' FROM dbo.DataHistoryArticle

--DELETE FROM dbo.NewsArticle
--DBCC CHECKIDENT('dbo.NewsArticle',RESEED,0);
--DELETE FROM dbo.DataHistoryArticle
--DBCC CHECKIDENT('dbo.DataHistoryArticle',RESEED,0);
--DELETE FROM dbo.SourceInfoModel
--DBCC CHECKIDENT('dbo.SourceInfoModel',RESEED,0);
--SELECT * FROM dbo.NewsArticle
ROLLBACK TRAN

go

BEGIN TRAN


ROLLBACK TRAN