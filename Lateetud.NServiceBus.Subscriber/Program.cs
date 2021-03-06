﻿
using System;
using System.Configuration;
using System.Collections.Generic;
using Lateetud.NServiceBus.Common;
using Lateetud.NServiceBus.Common.Models.NECGeneralAgent;
using System.ServiceProcess;

namespace Lateetud.NServiceBus.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] serviceBase;
            serviceBase = new ServiceBase[]
            {
                new Lateetud()
            };
            ServiceBase.Run(serviceBase);



            //new Lateetud().debug();

            //var key = Console.ReadKey();

        }

        public void ServiceConfig()
        {
            MsmqSqlDBConfiguration msmqsqldbconfig = new MsmqSqlDBConfiguration(ConfigurationManager.ConnectionStrings["SqlPersistence"].ConnectionString, 1, 5);

            List<PublisherEndpoints> publisherEndpoints = new List<PublisherEndpoints>();
            publisherEndpoints.Add(new PublisherEndpoints(endpointName: "NEC.GeneralAgent.Publisher", messageType: typeof(NECGeneralAgent)));
            var endpointConfiguration = msmqsqldbconfig.ConfigureEndpoint("NEC.GeneralAgent.Subscriber", publisherEndpoints);
            msmqsqldbconfig.CreateEndpointInitializePipeline(endpointConfiguration).GetAwaiter().GetResult();

        }
    }
}
