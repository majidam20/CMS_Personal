using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Weather
/// </summary>
namespace DataAccess
{
    public class Weather
    {
        private string _currentCondition = null;
        private string _condCode = null;
        private string _currentTemp = null;
        private string _currentHighTemp = null;
        private string _currentLowTemp = null;

        public string CurrentCondition { get { return _currentCondition; } set { _currentCondition = value; } }
        public string CondCode { get { return _condCode; } set { _condCode = value; } }
        public string CurrentTemp { get { return _currentTemp; } set { _currentTemp = value; } }
        public string CurrentHighTemp { get { return _currentHighTemp; } set { _currentHighTemp = value; } }
        public string CurrentLowTemp { get { return _currentLowTemp; } set { _currentLowTemp = value; } }
        public string CurrentWeatherImage { get; set; }
        System.Xml.XmlDocument xmlDoc = null;
        System.Xml.XmlNode xmlNode = null;
        public Weather(string WeatherCode, string Culture)
        {
            xmlDoc = new System.Xml.XmlDocument();
            try
            {
                xmlDoc.Load(String.Format("http://xml.weather.yahoo.com/forecastxml?p={0}&u={1}", WeatherCode, Culture == "fa-IR" ? "c" : "f"));
                //xmlDoc.Load("http://xml.weather.yahoo.com/forecastxml?p=IRXX0003&u=c");
                //xmlDoc.Load((new Page()).MapPath("~/forecastxml.xml"));
                xmlNode = xmlDoc.DocumentElement.ChildNodes[0];
                switch (Culture)
                {
                    case "fa-IR":
                        {
                            CurrentCondition = new Business.Translate().FaWeatherByCode(xmlNode.ChildNodes[2].ChildNodes[0].Attributes["code"].Value); break;
                        }
                    case "en-US":
                        {
                            CurrentCondition = xmlNode.ChildNodes[2].ChildNodes[0].Attributes["title"].Value; break;
                        }
                }

                CondCode = xmlNode.ChildNodes[2].ChildNodes[0].Attributes["code"].Value;
                CurrentTemp = xmlNode.ChildNodes[2].Attributes["temp"].Value;
                CurrentHighTemp = xmlNode.ChildNodes[3].ChildNodes[0].ChildNodes[0].Attributes["high"].Value;
                CurrentLowTemp = xmlNode.ChildNodes[3].ChildNodes[0].ChildNodes[0].Attributes["low"].Value;

                string time = xmlNode.ChildNodes[2].Attributes["time"].Value.Replace("IRST", " ");
                DateTime sunRise = DateTime.Parse(xmlDoc.DocumentElement.ChildNodes[0].ChildNodes[2].Attributes["sunrise24"].Value);
                DateTime sunSet = DateTime.Parse(xmlDoc.DocumentElement.ChildNodes[0].ChildNodes[2].Attributes["sunset24"].Value);
                DateTime condTime = DateTime.Parse(time.Replace("IRST", "").Replace("IRT", ""));
                string strDN = null;
                if ((condTime > sunSet) || (condTime < sunRise))
                {
                    strDN = "n";
                }
                else
                {
                    strDN = "d";
                }
                CurrentWeatherImage = "http://l.yimg.com/a/i/us/nws/weather/gr/" + CondCode + strDN + ".png";
            }
            catch (System.Xml.XmlException xmlE)
            {
                throw xmlE;
            }
        }
    }
}