using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using StepTracker.Services;

namespace StepTracker.Model
{
    public class Person : INotifyPropertyChanged
    {
        private static readonly string _path = $"{Environment.CurrentDirectory}\\TestData";
        private static List<Person> _personsList = new List<Person>();
        private static readonly List<Person> _allPersonsList = new List<Person>();
        private static IOFileService _ioFileService;

        public event PropertyChangedEventHandler PropertyChanged;

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
                try
                {
                    _personsList = _ioFileService.LoadData();
                    _allPersonsList.AddRange(_personsList);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            return _allPersonsList;
        }
    }
}
