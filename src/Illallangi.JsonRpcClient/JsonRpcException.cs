using System;

namespace Illallangi
{
    public class JsonRpcException : Exception
    {
        public JsonRpcException(JsonRpcClient jsonRpcClient, JsonRpcError error) : base($"{jsonRpcClient.Uri} returned Error {error.Code}: {error.Message}")

        {
            this.Uri = jsonRpcClient.Uri;
            this.Code = error.Code;
            this.ErrorMessage = error.Message;
        }

        public Uri Uri { get; }
        public int Code { get; }
        public string ErrorMessage { get; }
    }
}