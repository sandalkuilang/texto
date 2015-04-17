IF (@Status = 'R') -- R its mean Restore
BEGIN
	DELETE FROM dbo.MessageStatus
	WHERE SeqNbr = @SeqNbr
	AND Phonenumber = @Sender
END
ELSE
BEGIN
	IF (EXISTS(SELECT 1 FROM dbo.MessageStatus
	WHERE SeqNbr = @SeqNbr
	AND Phonenumber = @Sender))
	BEGIN
		UPDATE MessageStatus
		SET Status = @Status
		WHERE SeqNbr = @SeqNbr
		AND Phonenumber = @Sender
	END
	ELSE
	BEGIN	
		INSERT INTO dbo.MessageStatus
		SELECT @Sender, @SeqNbr, @Status, @Source
	END
END
