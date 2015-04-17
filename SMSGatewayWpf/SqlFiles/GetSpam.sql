 SELECT  Sender ,
		d.Name	AS [From] ,
        a.SeqNbr ,
        Message ,
        Time ,
        Status
FROM    dbo.Inbox a
        LEFT JOIN dbo.MessageStatus b ON a.Sender = b.Phonenumber
                                         AND a.SeqNbr = b.SeqNbr
        LEFT JOIN PhoneNumber c ON CASE WHEN SUBSTRING(c.PhoneNumber, 1, 1) = '0'
                                        THEN SUBSTRING(c.PhoneNumber, 2,
                                                       LEN(c.PhoneNumber))
                                        ELSE SUBSTRING(c.PhoneNumber, 3,
                                                       LEN(c.PhoneNumber))
                                   END = CASE WHEN SUBSTRING(Sender, 1, 1) = '0'
                                              THEN SUBSTRING(Sender, 2,
                                                             LEN(Sender))
                                              ELSE SUBSTRING(Sender, 3,
                                                             LEN(Sender))
                                         END
        LEFT JOIN dbo.ContactName d ON c.IDContact = d.ID
WHERE   b.Status = 'S'
ORDER BY Time DESC 