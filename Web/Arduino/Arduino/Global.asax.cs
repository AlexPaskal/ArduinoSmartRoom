using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO.Ports;

namespace Arduino
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Serial = new SerialPort("COM11", 9600);
            Serial.DataReceived += new SerialDataReceivedEventHandler(SerialRX);
            Serial.Open();
            Led = false;
        }

        private static bool _Led;
        public static bool Led
        {
            get
            {
                return _Led;
            }
            set
            {
                _Led = value;
                if (_Led)
                {
                    data = "true";
                }
                else
                {
                    data = "false";
                }
            }
        }
        public static string data = "";
        public static SerialPort Serial;
        static void SerialRX(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine();
            if (indata == "LOW")
            {
                Led = false;
            }
            if (indata == "HIGH")
            {
                Led = true;
            }
        }
    }
}
