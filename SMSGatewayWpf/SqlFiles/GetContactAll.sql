SELECT a.IDContact AS ID,
		b.Name,
		a.PhoneNumber
FROM dbo.PhoneNumber a 
	INNER JOIN dbo.ContactName b ON a.IDContact = b.ID
ORDER BY Name ASC	