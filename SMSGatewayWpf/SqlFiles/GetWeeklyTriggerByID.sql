SELECT  ID ,
        RecursEvery ,
        Sunday ,
        Monday ,
        Tuesday ,
        Wednesday ,
        Thursday ,
        Friday ,
        Saturday
FROM    dbo.WeeklyTrigger
WHERE   ID = @ID