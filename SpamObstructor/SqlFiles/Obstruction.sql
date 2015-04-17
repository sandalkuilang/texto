INSERT INTO dbo.MessageStatus
        ( Phonenumber, SeqNbr, Status, Source )         
SELECT Sender, SeqNbr, 'S', 'I' FROM dbo.Inbox
WHERE SeqNbr NOT IN (SELECT SeqNbr FROM dbo.MessageStatus WHERE Status = 'S')
	AND Sender IN (SELECT Phonenumber FROM dbo.MessageStatus WHERE Status = 'S')	