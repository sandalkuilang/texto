using GSMCommunication.PDUDecoder;
using System;

/// <summary>
/// Implements the base for an information element.
/// </summary>
namespace GSMCommunication.PDUDecoder.SmartMessaging
{
    public abstract class InformationElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GSMCommunication.PDUDecoder.SmartMessaging.InformationElement" /> class.
        /// </summary>
        protected InformationElement()
        {
        }

        /// <summary>
        /// In the derived classes, returns the byte array equivalent of this instance.
        /// </summary>
        /// <returns>The byte array.</returns>
        public abstract byte[] ToByteArray();

        /// <summary>
        /// Returns the string equivalent of this instance, which is a hexadecimal representation of the element.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return Calc.IntToHex(this.ToByteArray());
        }
    }
}