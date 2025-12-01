using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public partial class Strings
    {
        [YamlMember(Alias = "name", ApplyNamingConventions = false)]
        public string Name { get; set; }

        [YamlMember(Alias = "article_type", ApplyNamingConventions = false)]
        public ArticleType ArticleType { get; set; }

        [YamlMember(Alias = "singular_name", ApplyNamingConventions = false)]
        public string SingularName { get; set; }

        [YamlMember(Alias = "plural_name", ApplyNamingConventions = false)]
        public string PluralName { get; set; }

        [YamlMember(Alias = "description", ApplyNamingConventions = false, ScalarStyle = ScalarStyle.Literal)]
        public string Description { get; set; }
    }
}
