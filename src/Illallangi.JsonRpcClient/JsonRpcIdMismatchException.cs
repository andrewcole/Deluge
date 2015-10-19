using System;

namespace Illallangi
{
    public class JsonRpcIdMismatchException : Exception
    {
        public JsonRpcIdMismatchException() : base("JSON-RPC Response ID does not match request ID")
        {
        }
    }
}