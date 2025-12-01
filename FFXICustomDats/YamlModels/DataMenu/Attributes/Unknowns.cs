namespace FFXICustomDats.YamlModels.DataMenu.Attributes
{
    public partial struct Unknowns
    {
        public double? Double;
        public string String;

        public static implicit operator Unknowns(double Double) => new Unknowns { Double = Double };
        public static implicit operator Unknowns(string String) => new Unknowns { String = String };
    }
}
