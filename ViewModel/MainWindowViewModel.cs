using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using StepTracker.Model;
using StepTracker.Services;

namespace StepTracker.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly string _path = $"{Environment.CurrentDirectory}\\TestData";
        private Data _selectedDataItem;
        private IOFileService _ioFileService;

        public SeriesCollection SeriesCollection { get; set; }

        public List<Data> Data { get; }

        public MainWindowViewModel()
        {
            var allPersons = Person.GetPersons();

            var groupRecords = (from record in allPersons
                group record by record.User into g
                let average = g.Average(u => u.Steps)
                let best = g.Max(u => u.Steps)
                let worst = g.Min(u => u.Steps)
                select new Data
                {
                    UserName = g.Key,
                    AverageNumberOfSteps = (int)average,
                    BestResult = best,
                    WorstResult = worst,
                    Steps = (
                            from r in allPersons where r.User == g.Key select r.Steps)
                        .Take(new DirectoryInfo(_path).GetFiles().Length).ToList(),
                    Rank = (
                        from r in allPersons where r.User == g.Key select r.Rank).ToList().First(),
                    Status = (
                        from r in allPersons where r.User == g.Key select r.Status).ToList().First()
                }).ToList();
            Data = groupRecords;
        }

        public Data SelectedDataItem
        {
            get => _selectedDataItem;
            set
            {
                _selectedDataItem = value;

                SeriesCollection = new SeriesCollection
                {
                    new LineSeries {Values = _selectedDataItem.Steps.AsChartValues()}
                };
            }
        }
    }
}
