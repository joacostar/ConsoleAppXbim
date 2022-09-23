using System;
using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;


namespace ConsoleAppXbim
{
    public class Program
    {
        /// <summary>
        /// Simple console app to output some information from an element within an IFC model.
        /// </summary>
        static void Main()
        {
            Console.WriteLine("TESTING OF IFC METHODS...");

            //Revise file address and element GUID
            string fileName = @"C:\Users\Joaquin\Downloads\SimpleFromRevit_IFC4_DTV_All.ifc";
            string elementId = "23GawmFbfC$eZDeEszV2pY";

            Console.WriteLine($"IFC file {fileName}");            

            using (IfcStore model = IfcStore.Open(fileName))
            {
                var element = model.Instances.OfType<IIfcProduct>()
                    .Where(e => e.GlobalId == elementId)
                    .FirstOrDefault();

                Console.WriteLine($"Element GUID = {element.GlobalId}");
                Console.WriteLine($"Element type = {element.ExpressType}");
                Console.WriteLine($"Element name = {element.Name}");

                var quantities = element.IsDefinedBy
                    .SelectMany(r => r.RelatingPropertyDefinition.PropertySetDefinitions)
                    .OfType<IIfcElementQuantity>()
                    .SelectMany(qset => qset.Quantities);

                foreach (var q in quantities)
                {
                    Console.WriteLine($"Quantity name = {q.Name}\t\tQuantity type = {q.ExpressType}");
                }
            }
        }
    }
}