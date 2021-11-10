using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace flat_space.Lexor
{
    public class FileReader
    {
        private String _path;
        public FileReader(String path)
        {
            this._path = path;
        }

        public ViewModel readFromFile()
        {
            var extension = Path.GetExtension(_path);
            switch (extension)
            {
                case ".csv":
                    return new CSVAnalyzer().ParseFromFile(_path);

                case ".xml":
                    return new XMLAnalyzer().ParseFromFile(_path);
                
                default:
                    return new WebAnalyzer().ParseFromFile(_path);
            }
            //TODO: juiste error afhandeling maken (crash voorkomen)
        }
    }
}
