using Microsoft.Extensions.Configuration;
using System.Diagnostics;


namespace FFXICustomDats
{
    public class ExportDats(IConfiguration config)
    {
        private readonly string _originalData = config.GetValue<string>("OriginalData") ?? string.Empty;
        private readonly string _ffxiPath = config.GetValue<string>("FFXIPath") ?? string.Empty;

        public void ExportDatToYaml()
        {
            ExportDatToYaml(@"ROM\118\109.dat", @"items\armor.yml");
            ExportDatToYaml(@"ROM\286\73.dat", @"items\armor2.yml");
            //ExportDatToYaml(@"ROM\118\106.DAT", @"items\general_items.yml");
            ExportDatToYaml(@"ROM\301\115.dat", @"items\general_items2.yml");
            ExportDatToYaml(@"ROM\118\110.dat", @"items\puppet_items.yml");
            ExportDatToYaml(@"ROM\118\107.dat", @"items\usable_items.yml");
            ExportDatToYaml(@"ROM\118\108.dat", @"items\weapons.yml");
            ExportDatToYaml(@"ROM\118\114.dat", @"data_menu.yml");
            ExportDatToYaml(@"ROM\169\75.dat", @"merits.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        private void ExportDatToYaml(string datFile, string yamlFile)
        {
            Console.WriteLine($"Exporting {datFile} to {yamlFile}");

            var ffxiPath = $@"{_ffxiPath}\{datFile}";
            var outPath = $@"{_originalData}\{yamlFile}";

            if (!Path.Exists(outPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath) ?? string.Empty);
            }

            if (Path.Exists(ffxiPath))
            {
                Process process = new()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = Directory.GetCurrentDirectory().ToString(),
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        FileName = @"3rdPartyTools\xi-tinkerer-cli.exe",
                        Arguments = $@"export-dat ""{ffxiPath}"" ""{outPath}""",
                        RedirectStandardOutput = true
                    }
                };
                process.Start();
                process.WaitForExit();
                Console.WriteLine(process.StandardOutput.ReadToEnd());
            }
        }
    }
}
