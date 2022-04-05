namespace FontAwesome.Generator.Shared.Models
{
    public class FontAwesomeIcon
    {
        public string id { get; set; }
        public string label { get; set; }
        public string unicode { get; set; }
        public string unicodeString => FixUnicode(unicode);

        public List<string> styles { get; set; }
        public Dictionary<string, FontAwesomeSvgIcon> svg { get; set; }
        public List<string> free { get; set; }

        private string FixUnicode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            if (str.Length < 4)
            {
                for (int i = str.Length; i < 4; i++)
                {
                    str = "0" + str;
                }

                return "\\u" + str;
            }
            else if (str.Length == 4)
            {
                return "\\u" + str;
            }

            for (int i = str.Length; i < 8; i++)
            {
                str = "0" + str;
            }

            return "\\U" + str;
        }
    }
}
