USE TestDevALL
-- WORKING SELECT 
BEGIN TRAN

SELECT COUNT(*) as 'infoCOunt','source' FROM dbo.SourceInfoModel
UNION ALL		
SELECT COUNT(*) as 'infoCOunt','news' FROM dbo.NewsArticle
UNION ALL		
SELECT COUNT(*) as 'infoCOunt','history' FROM dbo.DataHistoryArticle
UNION ALL		
SELECT COUNT(*) as 'infoCOunt','_NewsCategoryCR' FROM dbo.DC_NewsCategoryCR

--EXEC PR_InsertHistory

SELECT COUNT(*) as 'infoCOunt','source' FROM dbo.SourceInfoModel
UNION ALL		
SELECT COUNT(*) as 'infoCOunt','news' FROM dbo.NewsArticle
UNION ALL		
SELECT COUNT(*) as 'infoCOunt','history' FROM dbo.DataHistoryArticle
UNION ALL		
SELECT COUNT(*) as 'infoCOunt','_NewsCategoryCR' FROM dbo.DC_NewsCategoryCR


ROLLBACK TRAN

go

BEGIN TRAN


ROLLBACK TRAN

/*

SELECT COUNT(*) as 'source' FROM dbo.SourceInfoModel
--SELECT * FROM dbo.SourceInfoModel
SELECT COUNT(*) as 'news' FROM dbo.NewsArticle
--SELECT * FROM dbo.NewsArticle
SELECT COUNT(*) as 'history' FROM dbo.DataHistoryArticle
--SELECT * FROM dbo.DataHistoryArticle
SELECT COUNT(*) as '_NewsCategoryCR' FROM dbo.DC_NewsCategoryCR
--SELECT * FROM dbo.DC_NewsCategoryCR
*/