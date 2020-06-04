using System.IO;
using System.Collections.Generic;
using System.Linq;
using SpaceTaxi_1.Utilities;

namespace SpaceTaxi_1.LevelLoading {
    public class Reader {
        public List<char> MapData {get; private set;}
        public Dictionary<char, string> MetaData {get; private set;}
        public Dictionary<char, string> LegendData {get; private set;}
        public List<string[]> CustomerData {get; private set;}
        



        public Reader() {
            MapData = new List<char>();
            MetaData = new Dictionary<char, string>();
            LegendData = new Dictionary<char, string>();
            CustomerData = new List<string[]>();            
        }

        public void ReadFile(string filename) {
            // Get each line of file as an entry in array lines.
            string[] lines = File.ReadAllLines(Utils.GetLevelFilePath(filename));
            
            // Iterate over lines and add data till the corresponding field
            // Adds tiles to MapData
            for (var i = 0; i < 24; i++) {
                foreach (var j in lines[i]) {
                    MapData.Add(j);
                }  
            }
            
            
            foreach (var i in lines) {
                
                if (i.StartsWith("Platforms")) {
                    var platforms = i.Split(':')[1].Split(',');
                    foreach (var j in platforms) {
                        var elm = lines.Where(el => el.StartsWith(j.Trim() + ") "));
                        var charKey = elm.First().Split(' ', ')')[0].ToCharArray()[0];
                        var strValue = elm.First().Split(' ')[1];
                        MetaData.Add(charKey, strValue);
                    } 
                } 
                
                if (i.Contains(")")) {
                    char charKey = i.Split(')')[0][0];
                    string strValue = i.Split(')')[1].Trim();
                    LegendData.Add(charKey, strValue);
                } 

                if (i.StartsWith("Customer")) {
                    var customers = i.Split(':')[1].Split(' ');                            
                    CustomerData.Add(customers);                    
                }                             
            }
            
        }
    }
}
