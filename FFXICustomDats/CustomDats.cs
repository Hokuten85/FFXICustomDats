namespace FFXICustomDats
{
    public class CustomDats(PatchYaml patchYaml, ExportDats exportDats)
    {
        private readonly PatchYaml _patchYaml = patchYaml;
        private readonly ExportDats _exportDats = exportDats;

        public void MainMenu()
        {
            bool endApp = false;

            while (!endApp)
            {
                Console.Clear();
                Console.WriteLine("FFXI Dat Generator");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("\t1 - Export Dats to Yaml");
                Console.WriteLine("\t2 - Apply xidb data to Yaml");
                Console.WriteLine("\t3 - Apply patch file to Yaml");
                Console.WriteLine("\t4 - Create Dats");
                Console.WriteLine("\t5 - Clear GenerateYaml directory");
                Console.WriteLine("\t6 - Quit");
                Console.WriteLine();
                Console.WriteLine("Type a number, and then press Enter:");

                if (int.TryParse(Console.ReadLine(), out int numInput))
                {
                    switch (numInput)
                    {
                        case 1:
                            _exportDats.ExportDatToYaml(); 
                            break;
                        case 2:
                            _patchYaml.PatchYamlFromXidb();
                            break;
                        case 3:
                            _patchYaml.PatchYamlFromFiles();
                            break;
                        case 4:
                            //_writeDats.WriteNewYaml();
                            break;
                        case 5:
                            _patchYaml.ClearGenerateYamlDir();
                            break;
                        case 6:
                            endApp = true;
                            break;
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
