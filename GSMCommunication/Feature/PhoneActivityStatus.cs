using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMCommunication.Feature
{
    /// <summary>
    /// Activity status of the mobile equipment
    /// </summary>
    public enum PhoneActivityStatus
    {
        /// <summary>
        /// Allow commands from TA/TE.
        /// </summary>
        Ready,
        /// <summary>
        /// Does not allow commands.
        /// </summary>
        Unavailable,
        Unknown,
        /// <summary>
        /// Ringer is active
        /// </summary>
        Ringing,
        CallInProgress,
        /// <summary>
        /// Low functionality
        /// </summary>
        Sleep
    }
}