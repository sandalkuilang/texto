UPDATE dbo.ContactName
SET Name = @Name,
	Note = ''
WHERE ID = @ID	

UPDATE dbo.PhoneNumber
SET PhoneNumber = @PhoneNumber
WHERE IDContact = @ID
	AND IDPrefixPhoneName = 500