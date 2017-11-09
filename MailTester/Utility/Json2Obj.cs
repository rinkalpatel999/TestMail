using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MailTester.Utility
{
    public static class Json2Obj<T>
    {
        public static readonly Dictionary<string, object> CachedConfig = new Dictionary<string, object>();

        public static T Load()
        {
            var path = Path.GetFullPath(Helpers.AssemblyDirectory + @"\Configs\\");
            return Load(path + typeof(T).Name + ".json");
        }

        public static T Load(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                return Activator.CreateInstance<T>();
            }
            var jsonFile = File.ReadAllText(jsonPath);
            var obj = JsonConvert.DeserializeObject<T>(jsonFile, JsonSerializerSettings);
            return obj;
        }

        public static void Save(T settings)
        {
            Save(Path.GetFullPath(Helpers.AssemblyDirectory + @"\Configs\\") + typeof(T).Name + ".json", settings);
        }

        public static bool IsExist()
        {
            return
                File.Exists(Path.GetFullPath(Helpers.AssemblyDirectory + @"\Configs\\") + typeof(T).Name + ".json");
        }

        public static void Save(string jsonPath, T settings)
        {
            //Update cached value
            if (CachedConfig.ContainsKey(typeof(T).Name))
            {
                CachedConfig.Remove(typeof(T).Name);
            }

            CachedConfig.Add(typeof(T).Name, settings);

            //Save to disk
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(settings));
        }

        /// <summary>
        /// Json serializer settings
        /// </summary>
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }
}