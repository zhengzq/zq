namespace Example.Core.Web
{
    public class Page<TOption>
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public TOption Option { get; set; }
    }
}