using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Converter;

namespace OPCFlex
{
    class LoaderCSV
    {
        public List<FlexEvent> DescriptionFlexList;
        public string Path;
        private List<string> m_files; 

        public LoaderCSV()
        {
            DescriptionFlexList = new List<FlexEvent>();
            m_files = new List<string>();
        }

        public void Load (string path)
        {
            Path = path;
            FilesAdd(Path);
            RecurseAdds(Path);

        }
        private void FilesAdd(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                m_files.Add(file);
                Console.WriteLine(file);
            }
        }

        private void RecurseAdds(string path)
        {
            var dirs = Directory.GetDirectories(path);

            foreach (var dir in dirs)
            {
                //Console.WriteLine(dir);
                FilesAdd(dir);
                RecurseAdds(dir);
            }
        }
    }
}
