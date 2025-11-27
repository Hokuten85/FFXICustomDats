using Microsoft.Extensions.Configuration;
using System.Diagnostics;


namespace FFXICustomDats
{
    public class ExportDats(IConfiguration config)
    {
        private readonly IConfiguration _config = config;

        public void ExportDatToYaml()
        {
            ExportDatToYaml(@"ROM\118\109.dat", "armor.yml");
            ExportDatToYaml(@"ROM\286\73.dat", "armor2.yml");
            //ExportDatToYaml(@"ROM\118\106.DAT", "general_items.yml");
            ExportDatToYaml(@"ROM\301\115.dat", "general_items2.yml");
            ExportDatToYaml(@"ROM\118\110.dat", "puppet_items.yml");
            ExportDatToYaml(@"ROM\118\107.dat", "usable_items.yml");
            ExportDatToYaml(@"ROM\118\108.dat", "weapons.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        private void ExportDatToYaml(string datFile, string yamlFile)
        {
            Console.WriteLine($"Exporting {datFile} to {yamlFile}");

            var exportDatDir = _config.GetValue<string>("ExportDatDir");
            var ffxiPath = $@"{_config.GetValue<string>("FFXIPath")}\{datFile}";
            var outPath = $@"{exportDatDir}\{yamlFile}";

            if (!Path.Exists(exportDatDir))
            {
                Directory.CreateDirectory(exportDatDir);
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
