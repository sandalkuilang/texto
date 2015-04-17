SELECT  ID ,
        Days ,
        January ,
        February ,
        March ,
        April ,
        May ,
        June ,
        July ,
        August ,
        September ,
        October ,
        November ,
        December
FROM    dbo.MonthlyTrigger
WHERE ID = @ID