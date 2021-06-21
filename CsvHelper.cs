using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DotnetCsvHelper
{
    public interface ICsvHelper
    {
        List<T> ReadDataFromCsv<T>(String Path, bool ContainsHeaderRow = true, string Delimiter = ",");
        byte[] WriteDataToCsv<T>(List<T> Data);
    }
    public class CsvHelper : ICsvHelper
    {
        public List<T> ReadDataFromCsv<T>(string Path, bool ContainsHeaderRow = true, string Delimiter = ",")
        {
            var result = new List<T>();
            try
            {
                var lines = System.IO.File.ReadAllLines(Path);
                var type = typeof(T);
                var props = type.GetProperties();
                var data = lines.ToList();
                var header = new List<string>();
                if (ContainsHeaderRow)
                {
                    header = data[0].Split(Delimiter).ToList();
                    data.Remove(data[0]);
                }
                else
                {
                    header = props.Select(p => p.Name).ToList();
                }
                foreach (var item in data)
                {
                    var columnData = item.Split(Delimiter);
                    var obj = (T)Activator.CreateInstance(type);
                    for (var i = 0; i < header.Count(); i++)
                    {
                        var p = props.First(p => p.Name.ToLower() == header[i].ToLower());
                        if (!string.IsNullOrEmpty(columnData[i]))
                            obj.GetType().GetProperty(p.Name).SetValue(obj, Convert.ChangeType(columnData[i], p.PropertyType));
                    }
                    result.Add(obj);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public byte[] WriteDataToCsv<T>(List<T> Data)
        {
            try
            {
                var type = typeof(T);
                var props = type.GetProperties();
                using (var memStream = new MemoryStream())
                using (var streamWriter = new StreamWriter(memStream))
                {
                    streamWriter.WriteLine(String.Join(",", props.Select(p => p.Name).ToList()));
                    foreach (var item in Data)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var p in props)
                        {
                            var val = item.GetType().GetProperty(p.Name).GetValue(item)?.ToString();
                            if (val != null)
                                sb.Append(val);
                            sb.Append(",");
                        }
                        streamWriter.WriteLine(sb.ToString());
                    }

                    streamWriter.Flush();
                    return memStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}