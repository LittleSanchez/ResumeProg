using Newtonsoft.Json;
using ResumeProg.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeProg.Model
{
    public class SaveLoad
    {
        public const string PROGRAM_NAME = "ResumeProg";
        public static readonly string PROGRAM_FOLDER = Environment.GetEnvironmentVariable("TMP") + Path.DirectorySeparatorChar + PROGRAM_NAME;
        public const string SAVE_FILE_EXTENTION = "rpsave";
        public static readonly string SAVE_FILE_NAME = "resumeprog_temp." + SAVE_FILE_EXTENTION;
        public static readonly string SAVE_FILE_PATH = PROGRAM_FOLDER + Path.DirectorySeparatorChar + SAVE_FILE_NAME;
        public const string NEW_PROJECT_FILE = "new_project.rpsave";

        public string CustomFilePath { get; set; }

        static SaveLoad()
        {
            if (!Directory.Exists(PROGRAM_FOLDER))
                Directory.CreateDirectory(PROGRAM_FOLDER);
        }

        public bool IsSaveExists() => File.Exists(SAVE_FILE_PATH);
        public void Save(Info info, string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = string.IsNullOrEmpty(CustomFilePath) ? SAVE_FILE_PATH : CustomFilePath;
            else 
                CustomFilePath = path;
            StringWriter stringWriter = new StringWriter();
            new JsonSerializer().Serialize(stringWriter, info);
            File.WriteAllText(path, stringWriter.ToString());
        }

        public Info Load(string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = string.IsNullOrEmpty(CustomFilePath) ? SAVE_FILE_PATH : CustomFilePath;
            else
                CustomFilePath = path;
            if (!IsSaveExists()) 
                return new Info();
            string data = File.ReadAllText(path);
            StringReader stringReader = new StringReader(data);
            Info info = (Info) new JsonSerializer().Deserialize(stringReader, typeof(Info));
            return info;
        }

        public static void RemoveTempSave()
        {
            File.Delete(SAVE_FILE_PATH);
        }
    }
}
