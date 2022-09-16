using JsonCleanUp.Common;

namespace JsonCleanup.Domain
{
    public class RemoveNode : IRemoveNode
    {
        public void RemoveJsonNode(List<Dictionary<String, Object>> data)
        {
            foreach (var valuePairs in data)
            {
                foreach (var item in valuePairs)
                {
                    TryRemoveNode(item.Value);
                }
            }
        }

        private void TryRemoveNode(dynamic node)
        {
            bool IsRemoved = true;
            while (IsRemoved)
            {
                try
                {
                    foreach (var item in node)
                    {
                        if (StringExtensions.IsStringValid(item.Value.ToString()))
                        {
                            item.Remove();
                            IsRemoved = true;
                            break;
                        }
                        else
                            IsRemoved = false;
                    }
                }
                catch (Exception)
                {
                    IsRemoved = false;
                }
            }
        }
    }
}
