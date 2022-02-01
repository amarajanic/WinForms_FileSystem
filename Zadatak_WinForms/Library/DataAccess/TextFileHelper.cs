using Library.Interfaces;
using Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    public class TextFileHelper : IHelper<Osoba>
    {
        public string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Zadatak.txt";

        public List<Osoba> ReadFromFile()
        {
            List<Osoba> people = new List<Osoba>();
            List<string> lines = File.ReadAllLines(filePath).ToList();
            try
            {
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    Osoba person = new Osoba();

                    person.Ime = parts[0];
                    person.Prezime = parts[1];
                    person.JMBG = parts[2];
                    person.Adresa = parts[3];

                    people.Add(person);
                }
            }
            catch (Exception)
            {

            }
            return people.ToList();
        }

        public void WriteToFile(List<Osoba> people)
        {
            List<string> output = new List<string>();

            foreach (var p in people)
            {
                output.Add($"{p.Ime},{p.Prezime},{p.JMBG}, {p.Adresa}");
            }

            File.WriteAllLines(filePath, output);
        }
    }
}
