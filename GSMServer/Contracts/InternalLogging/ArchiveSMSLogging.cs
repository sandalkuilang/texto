using GSMCommunication.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Texaco.Database;

namespace GSMServer.Contracts.InternalLogging
{
    internal class ArchiveSMSLogging : ISMSLogging
    {
        public void Read(List<GSMCommunication.Feature.BaseResult<GSMCommunication.Feature.SMSReadResult>> arg)
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);

            string guid;
            foreach (BaseResult<SMSReadResult> read in arg)
            {
                guid = GUID.GenerateID("I");
                db.Execute("InsertSMSInbox", new
                {
                    Sequence = guid,
                    Sender = read.Response.From,
                    Message = read.Response.Message,
                    Time = read.Response.Sent,
                    ReceiverOperator = read.Response.Operator,
                    Error = read.Response.Error,
                    IsRead = 0
                });
            }
            db.Close();
        }

        public void Send(GSMCommunication.Feature.BaseResult<GSMCommunication.Feature.SMSSendResult> arg)
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);

            if (!string.IsNullOrEmpty(arg.Response.To))
            {
                db.Execute("InsertSMSOutbox", new
                {
                    Sequence = GUID.GenerateID("O"),
                    Sender = arg.Response.From == null ? string.Empty : arg.Response.From,
                    Receiver = arg.Response.To,
                    Message = arg.Response.Message,
                    Time = arg.Response.Sent,
                    SenderOperator = arg.Response.Operator,
                    NetworkStatus = arg.Response.NetworkStatus,
                    Error = arg.Response.Error
                });
                db.Close();
            }
        }
    }
}
