using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StepTracker.Model;

namespace StepTracker.Services
{
    class IOFileService
    {
        

        private readonly string _path;
        public  IOFileService (string path)
        {
            _path = path;
        }

        public List<Person> LoadData()
        {
            var fileExists = File.Exists(_path);

            if (!fileExists)
            {
                File.CreateText(_path).Dispose();
                return new List<Person>();
            }
            using (var reader = File.OpenText(_path))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Person>>(fileText);
            }
        }

    }
}
