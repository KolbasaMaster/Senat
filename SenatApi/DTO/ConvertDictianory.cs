using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;

namespace SenatApi
{
    internal class ConvertDictianory : IValueConverter<Dictionary<string, string>, string>
    {
        public string Convert(Dictionary<string, string> sourceMember, ResolutionContext context)
        {

            string result = JsonConvert.SerializeObject(sourceMember);
            return result;
        }
    }
}
