DELETE FROM dbo.ContactName
WHERE ID = @ID

DELETE FROM dbo.PhoneNumber
WHERE IDContact = @ID