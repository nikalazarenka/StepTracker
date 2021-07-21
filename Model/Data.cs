using System.Collections.Generic;
using System.ComponentModel;

namespace StepTracker.Model
{
    public class Data : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string UserName { get; set; }
        public int AverageNumberOfSteps { get; set; }
        public int BestResult { get; set; }
        public int WorstResult { get; set; }
        public int Rank { get; set; }
        public string Status { get; set; }

        public List<int> Steps { get; set; }

    }
}
