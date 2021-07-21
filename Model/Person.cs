using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using StepTracker.Services;

namespace StepTracker.Model
{
    public class Person
    {
        private static readonly string _path = $"{Environment.CurrentDirectory}\\TestData";
        private static List<Person> _personsList = new List<Person>();
        private static readonly List<Person> _allPersonsList = new List<Person>();
        private static IOFileService _ioFileService;

        public int Rank { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public int Steps { get; set; }

        public static List<Person> GetPersons()
        {
            var numberOfFiles = new DirectoryInfo(_path).GetFiles().Length;

            for (int i = 1; i <= numberOfFiles; i++)
            {
                _ioFileService = new IOFileService(_path + $"\\day{i}.json");

                _personsList = _ioFileService.LoadData();
                _allPersonsList.AddRange(_personsList);
            }

            return _allPersonsList;
        }
        

    }
}
