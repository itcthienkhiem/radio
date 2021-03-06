USE [RIS_GTVT]
GO
/****** Object:  StoredProcedure [dbo].[spGetTestListFromPid]    Script Date: 02/17/2012 13:34:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spGetTestListFromPid]
(
    @pID    NVARCHAR(20)
    
)
AS
	DECLARE @patientId BIGINT
SELECT @patientId = (
           SELECT TOP 1 lpi.Patient_ID
           FROM   L_PATIENT_INFO lpi
           WHERE  lpi.PID = @pID
       )
--SELECT @patientId
DECLARE @pTestId BIGINT
SELECT @pTestId = (
           SELECT TOP 1 tti.Test_ID
           FROM   T_TEST_INFO tti
           WHERE  tti.Patient_ID = @patientId
       )

SELECT trl.Alias_Name AS CodeMeaning,
       (
           SELECT TOP 1 ddc.[Description]
           FROM   D_DATA_CONTROL ddc
           WHERE  ddc.Device_ID = trl.Device_ID
                  AND ddc.Alias_Name = trl.Alias_Name
       ) AS CodeValue
FROM   T_REG_LIST trl
WHERE  trl.Test_ID = @pTestId



