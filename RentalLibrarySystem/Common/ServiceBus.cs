using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ServiceBus
    {
        public static readonly string Url  = "rabbitmq://localhost/";
        public static readonly string UserName = "guest";
        public static readonly string Password = "guest";

        public static class QueueNames
        {
            public static readonly string identityQueue = "identity-queue";
            public static readonly string memberQueue = "member-queue";
            public static readonly string catalogQueue = "catalog-queue";
            public static readonly string borrowingQueue = "borrowing-queue";
        }
    }
}
