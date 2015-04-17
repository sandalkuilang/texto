SELECT  ID ,
        RecursEvery  
FROM    dbo.DailyTrigger a INNER JOIN QueueWorkItem b ON a.ID = b.ScheduleID
WHERE   a.ID = @ID 