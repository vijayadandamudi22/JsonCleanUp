namespace JsonCleanup.Domain
{
    public interface IRemoveNode
    {
        void RemoveJsonNode(List<Dictionary<String, Object>> data);
    }
}