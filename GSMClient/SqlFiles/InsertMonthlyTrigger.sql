INSERT INTO dbo.MonthlyTrigger
        ( ID ,
          Days ,
          January ,
          February ,
          March ,
          April ,
          May ,
          June ,
          July ,
          August ,
          September ,
          October ,
          November ,
          December
        )
VALUES  ( @ID , -- ID - varchar(20)
          @Days , -- Days - varchar(100)
          @January , -- January - int
          @February , -- February - int
          @March , -- March - int
          @April , -- April - int
          @May , -- May - int
          @June , -- June - int
          @July , -- July - int
          @August , -- August - int
          @September , -- September - int
          @October , -- October - int
          @November , -- November - int
          @December  -- December - int
        )