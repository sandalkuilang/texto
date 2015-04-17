DECLARE @@scheduleID VARCHAR(20),
		@@prefix VARCHAR(1)

SELECT @@scheduleID = ISNULL(ScheduleID, '')
FROM QueueWorkItem
WHERE SeqNbr = @SeqNbr

DELETE FROM QueueWorkItem
Where SeqNbr = @SeqNbr 

IF (@@scheduleID <> '')
BEGIN 
	SET @@prefix = SUBSTRING(@@scheduleID, 1, 1) 
  
	IF (@@prefix = 'D')
	BEGIN
		DELETE FROM DailyTrigger
		WHERE ID = @@scheduleID 
	END
	ELSE IF (@@prefix = 'W')
	BEGIN
		DELETE FROM WeeklyTrigger
		WHERE ID = @@scheduleID
	END
	ELSE IF (@@prefix = 'M')
	BEGIN
		DELETE FROM MonthlyTrigger
		WHERE ID = @@scheduleID
	END  
END
