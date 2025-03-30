using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Application.Contracts;
using Overhaul.Application.Enums;
using Overhaul.Application.Models.Sms;
using Overhaul.Application.Services.Sms;
using ir.afe.www;

namespace Overhaul.Infrastructure.ExternalServices
{
    public class SmsService : ISmsProvider
    {
        //private readonly BoxServiceSoap _boxServiceSoap;

        public SmsService(/*BoxServiceSoap boxServiceSoap*/)
        {
            //_boxServiceSoap = boxServiceSoap;
        }
        public async Task<bool> SendSms(string userName, string password, string number, string[] mobiles, string text)
        {
            var smsProvider = new BoxServiceSoapClient(BoxServiceSoapClient.EndpointConfiguration.BoxServiceSoap);

            var result = await smsProvider.SendMessageAsync(userName,password,number,new ArrayOfString { mobiles.FirstOrDefault() },text,"0");
            return true;
        }
    }
}
