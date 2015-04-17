INSERT INTO dbo.QueueWorkItem
        ( SeqNbr ,
		  Command , 
          ScheduleID ,
          Created ,
		  RecursPoint ,
          Enabled ,
		  Status
        )
VALUES  ( @SeqNbr , -- SeqNbr - varchar(20)
		  @Command , -- Command - varchar(2000) 
          @ScheduleID , -- ScheduleID - int
          @Created , -- Created - datetime
		  @Created , -- RecursPoint - datetime
          @Enabled ,  -- Enabled - bit
		  'R' -- Status varchar(1)
        )