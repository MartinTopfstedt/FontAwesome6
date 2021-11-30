namespace FontAwesome.Generator.Shared.Models.GraphQL
{
    public class Release
    {
        public string version { get; set; }

        public bool isLatest {  get; set; }
        public Icon[] Icons { get; set; }
    }
}
