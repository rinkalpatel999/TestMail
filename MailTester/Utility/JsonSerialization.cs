using System.Text;
using Newtonsoft.Json;

namespace MailTester.Utility
{
    public class JsonSerialization : ISerialization
    {
        public string Serialize(object obj, Encoding encoding)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public string Serialize(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public T Deserialize<T>(string json)
        {
            var testDto = JsonConvert.DeserializeObject<T>(json);
            return testDto;
        }
    }
}