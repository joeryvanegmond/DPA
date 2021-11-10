using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space
{
    public abstract class ILexicalAnalyzer
    {

        public abstract ViewModel ParseFromFile(String path);
        
    }
}