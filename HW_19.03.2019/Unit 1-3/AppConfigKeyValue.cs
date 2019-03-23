using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.SMN_182.Unit_1_3
{
    public class AppConfigKeyValuesDemo
    {
        public void ReadKeyValuesFromAppConfig()
        {
            var currencyServerEndpointValue = ConfigurationManager
                .AppSettings["currencyServerEndpoint"];

            Uri currencyServerEndpoint = new Uri(currencyServerEndpointValue);
        }
    }
}
