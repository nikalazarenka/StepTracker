using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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

        public void ExportDataJson(Data data)
        {
            using (StreamWriter writer = File.CreateText(_path))
            {
                string info = JsonConvert.SerializeObject(data);
                writer.Write(info);
            }
        }

        public void ExportDataXml(Data data)
        {
            XmlSerializer writer = new XmlSerializer(typeof(Data));
            using (FileStream fileStream = new FileStream(_path,FileMode.OpenOrCreate))
            {
                writer.Serialize(fileStream,data);
            }
        }

        public void ExportDataCsv(Data data)
        {
            var sb = new StringBuilder();
            var header = "";
            var into = typeof(Data).GetProperties();

            using (StreamWriter writer = new StreamWriter(new FileStream(_path, FileMode.OpenOrCreate), Encoding.UTF8))
            {

                foreach (var prop in typeof(Data).GetProperties())
                {
                    header += prop.Name + "; ";
                }

                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                writer.Write(sb.ToString());

                var sb2 = new StringBuilder();
                var line = "";
                foreach (var prop in into)
                {
                    line += prop.GetValue(data, null) + "; ";
                }

                line = line.Substring(0, line.Length - 2);
                sb2.AppendLine(line);
                writer.Write(sb2.ToString());
            }
        }

    }
}
