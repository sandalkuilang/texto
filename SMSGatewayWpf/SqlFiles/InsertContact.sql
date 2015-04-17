INSERT INTO dbo.ContactName
        ( ID, Name, Note )
VALUES  ( @ID, -- ID - varchar(10)
          @Name, -- Name - varchar(50)
          ''  -- Note - varchar(2000)
          )
INSERT INTO dbo.PhoneNumber
        ( IDContact ,
          IDPrefixPhoneName ,
          PhoneNumber
        )
VALUES  ( @ID , -- IDContact - varchar(10)
          500 , -- IDPrefixPhoneName - varchar(3)
          @PhoneNumber  -- PhoneNumber - varchar(50)
        ) 