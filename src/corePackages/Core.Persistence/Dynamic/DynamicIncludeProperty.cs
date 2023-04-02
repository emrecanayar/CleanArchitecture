namespace Core.Persistence.Dynamic
{
    public class DynamicIncludeProperty
    {
        public Dynamic Dynamic { get; set; }
        public List<string> IncludeProperties { get; set; }

        public DynamicIncludeProperty()
        {
            IncludeProperties = new List<string>();
        }
    }
}
