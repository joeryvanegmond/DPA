using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace flat_space.Lexor
{
    class WebAnalyzer : ILexicalAnalyzer
    {
        public override ViewModel ParseFromFile(String url)
        {
            var response = clientResponse(url).Result;
            //TODO: deze check op xml of csv moet sterker
            if (response[0] == '<')
            {
                return new XMLAnalyzer().ParseFromString(response);
            }
            return new CSVAnalyzer().ParseFromString(response);


        }

        public Task<string> clientResponse(String url) => Task.FromResult(new HttpClient().GetStringAsync(url).Result);
    }
}
