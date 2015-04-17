SELECT  'E' AS Source, -- its mean Edited
		 SeqNbr ,
         Command ,
         ScheduleID ,
         Created ,
         Enabled ,
		 RecursPoint ,
         LastExecuted ,
         NextExecuted ,
         Status
FROM dbo.QueueWorkItem
WHERE Enabled = 1 
ORDER BY Created ASC