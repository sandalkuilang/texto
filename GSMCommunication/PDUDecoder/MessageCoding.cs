using System;

/// <summary>
/// Data coding/Message class
/// </summary>
namespace GSMCommunication.PDUDecoder
{
	public class MessageCoding : DataCodingScheme
	{
		private bool bit3;

		private byte dataCoding;

		private byte messageClass;

		/// <summary>
		/// Gets the alphabet being used.
		/// </summary>
		public override byte Alphabet
		{
			get
			{
				return this.dataCoding;
			}
		}
          
		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		/// <param name="dcs">The DCS byte to decode.</param>
		public MessageCoding(byte dcs) : base(dcs)
		{
			object obj;
			this.bit3 = (dcs & 8) > 0;
			MessageCoding messageCoding = this;
			if ((dcs & 4) > 0)
			{
				obj = 1;
			}
			else
			{
				obj = null;
			}
			messageCoding.dataCoding = (byte)obj;
			this.messageClass = (byte)(dcs & 3);
		}
	}
}