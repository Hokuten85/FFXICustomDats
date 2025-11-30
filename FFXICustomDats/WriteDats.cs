using Microsoft.Extensions.Configuration;
using System.Diagnostics;


namespace FFXICustomDats
{
    public class WriteDats(IConfiguration config)
    {
        private readonly string _projectDir = config.GetValue<string>("XITinkerProject") ?? string.Empty;
        private readonly string _tinkerCLI = config.GetValue<string>("TinkerCLI") ?? string.Empty;
        private readonly string _generatedDats = config.GetValue<string>("GeneratedDats") ?? string.Empty;

        public void WriteYamlToDats()
        {
            if (!Path.Exists(_projectDir))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_projectDir) ?? string.Empty);
            }
            
            if(Path.Exists(_generatedDats))
            {
                foreach(FileInfo file in new DirectoryInfo(_generatedDats).EnumerateFiles())
                {
                    file.Delete();
                }
            }

            Process process = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    WorkingDirectory = Directory.GetCurrentDirectory().ToString(),
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = _tinkerCLI,
                    Arguments = $@"make-dats ""{_projectDir}""",
                    RedirectStandardOutput = true
                }
            };
            process.Start();
            process.WaitForExit();
            Console.WriteLine(process.StandardOutput.ReadToEnd());

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }
    }
}
