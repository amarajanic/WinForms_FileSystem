using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces
{
    interface IHelper<T> where T : class
    {
       List<T> ReadFromFile(string filePath);
       void WriteToFile(string filePath, List<T> people);
    }
}
