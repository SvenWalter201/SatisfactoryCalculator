using System;

namespace Backend
{
    class Error
    {
        public string Message { get; private set; }
        public int ErrorCode { get; private set; }

        Error(string message, int errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public static Error SaveFileChangedError => new Error("Save File couldn't be loaded, because Layout was changed", 1);
        public static Error InvalidCastError => new Error("The cast could not be completed", 2);
    }

    public class ItemLayoutChangedException : Exception
    {
        public override string Message => "ItemLayout was changed. SaveFile can not be loaded";
    }
}
