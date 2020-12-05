using System;

namespace Logic.Exceptions
{
    [Serializable]
    public class FieldBuilderNegativeOrZeroException : Exception
    {
        public FieldBuilderNegativeOrZeroException() { }

        public FieldBuilderNegativeOrZeroException(string message) : base(message) { }

        public FieldBuilderNegativeOrZeroException(string message, Exception inner) : base(message, inner) { }
    }
}