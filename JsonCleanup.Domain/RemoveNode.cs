namespace JsonCleanup.Domain
{
    public class RemoveNode : IRemoveNode
    {
        private const string Name = "name";
        private const string Hobbies = "hobbies";
        private const string Education = "education";
        private const string NA = "N/A";
        private const string Hypen = "-";

        public void RemoveJsonNode(dynamic node)
        {
            TryRemoveNode(node[Name]);
            TryRemoveNode(node[Hobbies]);
            TryRemoveNode(node[Education]);
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
                        if (String.IsNullOrEmpty(item.Value.ToString()) || String.Equals(item.Value.ToString(), NA, StringComparison.OrdinalIgnoreCase) || String.Equals(item.Value.ToString(), Hypen, StringComparison.OrdinalIgnoreCase))
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
