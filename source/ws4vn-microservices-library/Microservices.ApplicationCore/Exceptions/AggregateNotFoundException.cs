﻿using System;
using System.Runtime.Serialization;

namespace Ws4vn.Microservices.ApplicationCore.Exceptions
{
    [Serializable]
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException()
        {

        }

        public AggregateNotFoundException(string message) : base(message)
        {

        }

        public AggregateNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected AggregateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
