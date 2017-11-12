using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MatchXMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootDir = @"H:\OneDrive\Projects\BettingSerivce\FootballDataCollectionFork2\footballData\footballData";
            string[] fileNames = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);
            XmlParser parser = new XmlParser();

            foreach (var fileName in fileNames)
            {
                string ext = Path.GetExtension(fileName);
                if (ext == ".xml")
                {
                    Console.WriteLine(fileName);
                    try
                    {
                        parser.ParseMatch(fileName);
                    }
                    catch (XmlException ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    catch(NullReferenceException ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
           
        }
    }
}
