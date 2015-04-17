SELECT  Sender ,
        d.Name AS [From] ,
        a.SeqNbr ,
        Receiver ,
        e.Name AS [To] ,
        Message ,
        Time ,
        SenderOperator
FROM    dbo.Outbox a
        LEFT JOIN dbo.MessageStatus b ON a.Receiver = b.Phonenumber
                                         AND a.SeqNbr = b.SeqNbr
        LEFT JOIN PhoneNumber c ON CASE WHEN SUBSTRING(c.PhoneNumber, 1, 1) = '0'
                                  THEN SUBSTRING(c.PhoneNumber, 2,
                                                 LEN(c.PhoneNumber))
                                  ELSE SUBSTRING(c.PhoneNumber, 3, LEN(c.PhoneNumber))
                             END = CASE WHEN SUBSTRING(Sender, 1, 1) = '0'
                                        THEN SUBSTRING(Sender, 2, LEN(Sender))
                                        ELSE SUBSTRING(Sender, 3, LEN(Sender))
                                   END
        LEFT JOIN dbo.ContactName d ON c.IDContact = d.ID 
		LEFT JOIN PhoneNumber g ON CASE WHEN SUBSTRING(g.PhoneNumber, 1, 1) = '0'
                                  THEN SUBSTRING(g.PhoneNumber, 2,
                                                 LEN(g.PhoneNumber))
                                  ELSE SUBSTRING(g.PhoneNumber, 3, LEN(g.PhoneNumber))
                             END = CASE WHEN SUBSTRING(Receiver, 1, 1) = '0'
                                        THEN SUBSTRING(Receiver, 2, LEN(Receiver))
                                        ELSE SUBSTRING(Receiver, 3, LEN(Receiver))
                                   END
        LEFT JOIN dbo.ContactName e ON g.IDContact = e.ID 
WHERE   b.Status IS NULL
        AND ( b.Source = 'O'
              OR b.Source IS NULL
            )
ORDER BY Time DESC