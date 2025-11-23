using FFXICustomDats.Data;
using FFXICustomDats.YamlModels.Items;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using DatEntities = FFXICustomDats.Data.XiDatEntities;
using YamlItems = FFXICustomDats.YamlModels.Items;

namespace FFXICustomDats
{
    public class CustomDats(IConfiguration config, ParseYaml parseYaml, ExportDats exportDats, WriteDats writeDats)
    {
        private readonly IConfiguration _config = config;
        private readonly ParseYaml _parseYaml = parseYaml;
        private readonly ExportDats _exportDats = exportDats;
        private readonly WriteDats _writeDats = writeDats;

        public void MainMenu()
        {
            bool endApp = false;

            while (!endApp)
            {
                Console.Clear();
                Console.WriteLine("FFXI Dat Generator");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("\t1 - Parse Dats and Populate xidb tables");
                Console.WriteLine("\t2 - Read xidb tables and create Dats");
                Console.WriteLine("\t3 - Export Dats to Yaml");
                Console.WriteLine("\t4 - Quit");
                Console.WriteLine();
                Console.WriteLine("Type a number, and then press Enter:");

                if (int.TryParse(Console.ReadLine(), out int numInput))
                {
                    switch (numInput)
                    {
                        case 1:
                            _parseYaml.ParseYamlFiles();
                            break;
                        case 2:
                            _writeDats.WriteYamlToDats();
                            break;
                        case 3:
                            _exportDats.ExportDatToYaml();
                            break;
                        case 4:
                            endApp = true;
                            break;
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
