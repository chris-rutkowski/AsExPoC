using System;
using System.Runtime.Serialization;

namespace AsExPoC
{
    [Serializable()]
    public class ForcedExitException : Exception, ISerializable
    {
        public ForcedExitException()
        {
        }
    }
}