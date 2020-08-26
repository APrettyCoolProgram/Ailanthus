// ExternalFile.cs : Do things with external files [150327]

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ailanthus
{
  class ExternalFile
  {
    // Put data from a file into an array
    public static string[] FileToArray(string fileToOpen)
    {
      string[] workArray = File.ReadAllLines(fileToOpen);  // Read filelines into an array
      return (workArray);                                  // Return array
    }
  }
}
