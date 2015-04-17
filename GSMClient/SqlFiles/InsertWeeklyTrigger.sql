INSERT INTO dbo.WeeklyTrigger
        ( ID ,
          RecursEvery ,
          Sunday ,
          Monday ,
          Tuesday ,
          Wednesday ,
          Thursday ,
          Friday ,
          Saturday
        )
VALUES  ( @ID , -- ID - int
          @RecursEvery , -- RecursEvery - int
          @Sunday , -- Sunday - bit
          @Monday , -- Monday - bit
          @Tuesday , -- Tuesday - bit
          @Wednesday , -- Wednesday - bit
          @Thursday , -- Thursday - bit
          @Friday , -- Friday - bit
          @Saturday  -- Saturday - bit
        )   