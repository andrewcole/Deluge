namespace Illallangi
{
    public class JsonRpcResponse<T>
    {
        public JsonRpcError Error { get; set; } = null;
        public string Id { get; set; } = null;
        public T Result { get; set; } = default(T);
    }
}