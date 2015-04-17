/// <summary>
/// Specifies the type of the incoming message.
/// </summary>
namespace GSMCommunication.PDUDecoder
{
	public enum IncomingMessageType
	{
		/// <summary>Specifies that the message is an SMS-DELIVER.</summary>
		SmsDeliver,
		/// <summary>Specifies that the message is an SMS-STATUS-REPORT.</summary>
		SmsStatusReport,
		/// <summary>Specifies that the message is an SMS-SUBMIT-REPORT.</summary>
		SmsSubmitReport
	}
}