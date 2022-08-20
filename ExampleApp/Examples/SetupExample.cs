using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogSharper;

namespace ExampleApp.Examples
{
    public static class SetupExample
    {
        public static void Main()
        {
            LogSharper.LogSharper.Setup(true); //will show the message initialized message
            //cuntiniue with your app
        }
    }
}
