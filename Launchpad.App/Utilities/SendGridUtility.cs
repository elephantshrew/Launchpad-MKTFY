using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.App.Utilities
{
    public class SendGridUtility
    {
        /*
        private readonly IConfiguration _configuration;
        public SendGridUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        */

        private readonly string _apiKey;

        public SendGridUtility(string apiKey)
        {
            _apiKey = apiKey;
        }

        public void send()
        {
            throw new NotImplementedException("TODO");
        }



    }
}
