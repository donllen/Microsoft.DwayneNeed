﻿using System;
using System.Runtime.Serialization;

namespace Microsoft.DwayneNeed.Utilities
{
    [Serializable]
    public class CommandLineUsageException : Exception
    {
        public CommandLineUsageException()
        {
        }

        public CommandLineUsageException(string message)
            : base(message)
        {
        }

        public CommandLineUsageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CommandLineUsageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                //this.fUserName = info.GetString("fUserName");
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                // info.AddValue("fUserName", this.fUserName);
            }
        }
    }
}