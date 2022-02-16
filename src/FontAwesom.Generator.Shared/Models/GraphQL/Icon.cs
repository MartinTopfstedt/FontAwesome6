namespace FontAwesome.Generator.Shared.Models.GraphQL
{
    public class Icon
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Unicode { get; set; }
        public List<string> Styles { get; set; }
        public Membership Membership { get; set; }
    }
}
