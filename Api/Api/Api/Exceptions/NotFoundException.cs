﻿using System;
using System.Runtime.Serialization;

namespace Api.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {


        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string? message, Exception? innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
