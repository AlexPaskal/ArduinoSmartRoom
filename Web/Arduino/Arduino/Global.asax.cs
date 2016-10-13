using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO.Ports;
using System.Data.SqlClient;

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

        static void WriteDataToDatabase(string data)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Server=mysql.zzz.com.ua;Database=alexwebsite_zzz_com_ua;Uid=aw_database;Pwd=AWDBpa55w0rd;";
            SqlCommand command = new SqlCommand(
                String.Format("INSERT INTO Arduino (VALUE,MARK) VALUES ({0},'agent')", (data == "true" ? "1" : "0")), 
                conn);

            conn.Open();

            command.ExecuteReader();

            conn.Close();
            conn.Dispose();
        }
        static string ReadFromDatabase()
        {
            string res = "";

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Server=mysql.zzz.com.ua;Database=alexwebsite_zzz_com_ua;Uid=aw_database;Pwd=AWDBpa55w0rd;";
            SqlCommand command = new SqlCommand("SELECT * FROM Arduino WHERE MARK='web'", conn);
            conn.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                res = (string)reader["VALUE"];
            }

            conn.Close();
            conn.Dispose();


            return res;
        }
    }
}
