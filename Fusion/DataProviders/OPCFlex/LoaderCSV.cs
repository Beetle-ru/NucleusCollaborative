using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Converter;
using Implements;

namespace OPCFlex
{
    class LoaderCSV
    {
        public List<FlexEvent> DescriptionFlexList;
        public string Path;
        private readonly List<string> m_files;
        public const char Separator = ';';
        public string Destination;

        enum ConfigSections
        {
            Undefined = 0,
            ConfigSection = 1,
            ArgumentsSection = 2
        }

        public LoaderCSV(string destination)
        {
            DescriptionFlexList = new List<FlexEvent>();
            m_files = new List<string>();
            Destination = destination;
        }

        public void Load(string path)
        {
            Path = path;
            FilesAdd(Path);
            RecurseAdds(Path);
            foreach (var file in m_files)
            {
                ParseFile(file);
            }
        }

        public List<FlexEvent> LoadAndGet(string path)
        {
            Load(path);
            return DescriptionFlexList;
        }

        private void FilesAdd(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var element = file.Split('\\');
                var isIgnored = false;
                foreach (var e in element)
                {
                    if (e.StartsWith("-")) isIgnored = true;
                }
                if (!isIgnored)
                {
                    m_files.Add(file);    
                }
            }
        }

        private void RecurseAdds(string path)
        {
            var dirs = Directory.GetDirectories(path);

            foreach (var dir in dirs)
            {
                FilesAdd(dir);
                RecurseAdds(dir);
            }
        }

        private void ParseFile(string path)
        {
            string[] strings;
            try
            {
                strings = File.ReadAllLines(path);
            }
            catch
            {
                strings = new string[0];
                InstantLogger.err("Cannot read the file: {0}", path);
                return;
            }

            try
            {
                ConfigSections mode = ConfigSections.Undefined;
                var description = new FlexEvent();
                for (int strCnt = 0; strCnt < strings.Count(); strCnt++)
                {
                    string[] values = strings[strCnt].Split(Separator);
                    if (values.Any())
                    {
                        if (values[0] != "")
                        {
                            if (values[0] == ParseKeys.ConfFields) mode = ConfigSections.ConfigSection;
                            else if (values[0] == ParseKeys.ArgFields) mode = ConfigSections.ArgumentsSection;
                            else
                            {
                                switch (mode)
                                {
                                    case ConfigSections.ConfigSection:
                                        if (values.Count() >= 2)
                                        {
                                            if (values[0] == ParseKeys.FlagsKey)
                                            {
                                                description.Flags = (FlexEventFlag) Convertion.StrToInt32(values[1]);
                                            }
                                            if (values[0] == ParseKeys.OperationKey)
                                            {
                                                description.Operation = values[1];
                                            }
                                        }
                                    break;
                                    case ConfigSections.ArgumentsSection:
                                        if (values[0] == Destination)
                                        {
                                            if (values.Count() >= 3)
                                            {
                                                description.Arguments.Add(values[1], new Element(values[2]));
                                            }
                                        }
                                    break;
                                }
                            }
                        }
                    }
                }
                if (description.Arguments.Any())
                {
                    description.Id = Guid.NewGuid();
                    description.Time = DateTime.Now;
                    DescriptionFlexList.Add(description);
                }
                
            }
            catch (Exception e)
            {
                InstantLogger.err("Cannot parce the file: {0}, bad format call exeption: {1}", path, e.ToString());
                return;
            }
        }

        

        static class ParseKeys
        {
            // fields
            public const string ConfFields = "<Event>";
            public const string ArgFields = "<Arguments>";
            // keys
            public const string FlagsKey = "Flags";
            public const string OperationKey = "Operation";
        }
    }
}
