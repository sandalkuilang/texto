DELETE FROM Inbox
WHERE SeqNbr = @SeqNbr
AND Sender = @Sender

IF (@Receiver <> '')
BEGIN
	DELETE FROM Outbox
	WHERE SeqNbr = @SeqNbr
	AND Receiver = @Receiver

	
	DELETE dbo.MessageStatus
	WHERE SeqNbr = @SeqNbr
	AND Phonenumber = @Receiver
END

DELETE dbo.MessageStatus
WHERE SeqNbr = @SeqNbr
AND Phonenumber = @Sender