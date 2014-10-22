using EventCountdownUI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCountdownUI
{
    public class StringCreator
    {
        private Dictionary<string, string> parameters;

        public StringCreator()
        {
            parameters = new Dictionary<string, string>();
        }

        public StringCreator AddParameter(string parameter, object value)
        {
            var valueStr = value.ToString();
            if (!parameters.ContainsKey(parameter))
                parameters.Add(parameter, valueStr);
            else
                parameters[parameter] = valueStr;
            return this;
        }

        public StringCreator AddParameter(StringParam param, object value)
        {
            var paramStr = Enum.GetName(typeof(StringParam), param);
            return AddParameter(paramStr, value);
        }

        public string BuildString(string template)
        {
            var sb = new StringBuilder(template);

            foreach (var p in parameters)
            {
                sb = sb.Replace(p.Key, p.Value);
            }
            return sb.ToString();
        }
    }

    public enum StringParam
    {
        YEAR,
        MONTH,
        MONTH_NAME,
        MONTH_SHORT_NAME,
        DAY_SHORT_NAME,
        DAY_NAME,
        EVENT_NAME,
        DAY_COUNT,
        
    }
}
