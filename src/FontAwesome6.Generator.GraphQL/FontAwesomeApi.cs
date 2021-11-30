using FontAwesome6.Generator.GraphQL.Models;
using FontAwesome6.Generator.MetaData;
using FontAwesome6.Generator.Svgs;

using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FontAwesome6.GraphQl
{
    public class FontAwesomeApi
    {
        public async Task<Dictionary<string, FontAwesomeIcon>> GetIconsAsync(string root, string version)
        {
            var graphQLClient = new GraphQLHttpClient("https://api.fontawesome.com", new NewtonsoftJsonSerializer());
            var request = new GraphQLRequest
            {
                Query = @"query ReleaseQuery($version: String!) { 
                  release (version: $version)
                  {
                        version,
                            icons {
                            id,
                            label,
                            styles,
                            unicode,
                            membership {
                            free,
                            pro
                        }
                    }
                  }
                }",
                Variables = new
                {
                    version
                }
            };

            var response = await graphQLClient.SendQueryAsync<ResponseType>(request);

            var result = new Dictionary<string, FontAwesomeIcon>();
            foreach (var icon in response.Data.Release.Icons)
            {
                var faIcon = new FontAwesomeIcon
                {
                    unicode = icon.Unicode,
                    label = icon.Label,
                    styles = icon.Styles,
                    free = icon.Membership.Free,
                    svg = new Dictionary<string, Svg>()
                };

                foreach (var style in icon.Styles)
                {
                    var svgFile = Path.Combine(root, "svgs", style, icon.Id + ".svg");
                    if (!File.Exists(svgFile))
                    {
                        throw new FileNotFoundException(svgFile);
                    }

                    var serializer = new XmlSerializer(typeof(SvgFile));

                    using (var reader = XmlReader.Create(svgFile))
                    {
                        var svg = (SvgFile)serializer.Deserialize(reader);
                        faIcon.svg.Add(style, new Svg
                        {
                            height = svg.height,
                            width = svg.width,
                            raw = svg.defs?.style ?? "",
                            path = svg.paths.Select(p => p.d).ToArray()
                        });
                    }

                }


                result.Add(icon.Id, faIcon);
            }

            return result;
        }
    }
}
