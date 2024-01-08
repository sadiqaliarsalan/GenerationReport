using GenerationReport.Helper;
using GenerationReport.Interface;
using GenerationReport.Model;
using GenerationReport.Service;
using Microsoft.Extensions.Configuration;

namespace GenerationReport
{
    public class Program
    {
        private static IConfiguration _configuration { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _configuration = builder.Build();

            var inputPath = _configuration.GetSection("input").Value;
            MonitorDirectory(inputPath);

            Console.Write("press any key and enter to exit the application: ");
            Console.ReadLine();
        }

        private static void MonitorDirectory(string path)
        {
            FileSystemWatcher fileSystemWatcher = new();
            fileSystemWatcher.Path = path;
            fileSystemWatcher.Created += InitFileWatcher;
            fileSystemWatcher.Changed += InitFileWatcher;
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private static void InitFileWatcher(object sender, FileSystemEventArgs e)
        {
            ProcessXmlFile(e.FullPath);
        }

        private static void ProcessXmlFile(string filePath)
        {
            var outputPath = _configuration.GetSection("output").Value + "\\Output.xml";
            var referencePath = _configuration.GetSection("reference").Value + "\\Reference.xml";

            var inputGenerationReport = XmlHelper.DeserializeXml<GenerationReport.Model.GenerationReport>(filePath);
            var referenceData = XmlHelper.DeserializeXml<ReferenceData>(referencePath);

            var calculationService = new GeneratorCalculationService();

            var allGenerators = new List<IGeneratorCalculation>();
            allGenerators.AddRange(inputGenerationReport.Wind.WindGenerators.Cast<IGeneratorCalculation>());
            allGenerators.AddRange(inputGenerationReport.Gas.GasGenerators.Cast<IGeneratorCalculation>());
            allGenerators.AddRange(inputGenerationReport.Coal.CoalGenerators.Cast<IGeneratorCalculation>());
            var generatorTotals = calculationService.CalculateTotals(allGenerators, referenceData);

            var emissionGenerators = new List<IGeneratorCalculation>();
            emissionGenerators.AddRange(inputGenerationReport.Gas.GasGenerators.Cast<IGeneratorCalculation>());
            emissionGenerators.AddRange(inputGenerationReport.Coal.CoalGenerators.Cast<IGeneratorCalculation>());
            var dailyMaxEmissions = calculationService.CalculateDailyMaxEmissions(emissionGenerators, referenceData);

            var heatrates = new List<IGeneratorCalculation>();
            heatrates.AddRange(inputGenerationReport.Coal.CoalGenerators.Cast<IGeneratorCalculation>());
            var coalHeatRates = calculationService.CalculateHeatRates(heatrates);

            var generationOutput = new GenerationOutput
            {
                Totals = generatorTotals,
                MaxEmissionGenerators = dailyMaxEmissions,
                ActualHeatRates = coalHeatRates
            };

            XmlHelper.SerializeXml(generationOutput, outputPath);
        }
    }
}