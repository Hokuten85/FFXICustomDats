using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public partial class ElementCharge
    {
        [YamlMember(Alias = "fire", ApplyNamingConventions = false)]
        public int Fire { get; set; }

        [YamlMember(Alias = "ice", ApplyNamingConventions = false)]
        public int Ice { get; set; }

        [YamlMember(Alias = "wind", ApplyNamingConventions = false)]
        public int Wind { get; set; }

        [YamlMember(Alias = "earth", ApplyNamingConventions = false)]
        public int Earth { get; set; }

        [YamlMember(Alias = "lightning", ApplyNamingConventions = false)]
        public int Lightning { get; set; }

        [YamlMember(Alias = "water", ApplyNamingConventions = false)]
        public int Water { get; set; }

        [YamlMember(Alias = "light", ApplyNamingConventions = false)]
        public int Light { get; set; }

        [YamlMember(Alias = "dark", ApplyNamingConventions = false)]
        public int Dark { get; set; }
    }

    public static class ElementHelpers
    {
        public static bool IsEqual(int elementCharge, uint dbElements, Element element)
        {
            return elementCharge == GetPuppetElementValue(dbElements, element);
        }
        public static int GetPuppetElementValue(uint allElements, Element element)
        {
            return (int)(allElements >> ((int)(element - 1) * 4)) & 0xF;
        }

        public static uint ElementToBitValue(int elementValue, Element element)
        {
            return (uint)(elementValue & 0xF) << (((int)element - 1) * 4);
        }
    }
}
