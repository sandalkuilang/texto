SELECT  'E' AS Source,  -- its mean Edited
		 SeqNbr ,
         Command ,
         ScheduleID ,
         Created ,
         Enabled ,
         LastExecuted ,
         NextExecuted ,
         Status
FROM dbo.QueueWorkItem
WHERE Enabled = 0