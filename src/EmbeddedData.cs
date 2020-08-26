// EmbeddedData.cs : Do things with embedded data [150327]

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ailanthus
{
  class EmbeddedData
  {
    // Embedded list into array
    public static string[] ToArray(string listName)
    {
      Assembly eblAssembly = Assembly.GetExecutingAssembly();                                        // Object for the assembly - combine w/below? Public?

      StreamReader workStream =
        new StreamReader(eblAssembly.GetManifestResourceStream(listName));                           // List contents
      string [] workArray =
        workStream.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None); // Newline split
      
      return workArray;
    }

    // Embedded list data into a dictionary
    public static Dictionary<string, string> ToDictionary(string listName)
    {
      String[] keyValPair = new String[2];                                        // Temp array for key/values
      Dictionary<string,string> workDictionary = new Dictionary<string,string>(); // Work dictionary
      
      string [] listContents = ToArray(listName);                                 // Get list contents into an array
      
      foreach (var line in listContents)                                          // For each of the array elements...
	    {
        keyValPair = line.Split(new[] { "=" }, 2, StringSplitOptions.None);       // Split at first "=",
        workDictionary.Add(keyValPair[0], keyValPair[1]);                         // Put key/value in dictionary
	    }
      return workDictionary;
    }
  }
}
