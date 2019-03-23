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
/// Summary description for Translate
/// </summary>
namespace Business
{
    public class Translate
    {
        #region Weather Array
        string[][] strEnWeatherTypes = 
    {
        new string[]{"0", "tornado","طوفان"},
        new string[]{"1", "tropical storm","طوفان گرمسیری"},
        new string[]{"2", "hurricane","تندباد"},
        new string[]{"3", "severe thunderstorms","رعد و برق شدید"} ,
        new string[]{"4", "thunderstorms","رعد و برق شدید"} ,
        new string[]{"5", "mixed rain and snow","باران و برف"} ,
        new string[]{"6", "mixed rain and sleet","باران و تگرگ"} ,
        new string[]{"7", "mixed snow and sleet","برف و تگرگ"} ,
        new string[]{"8", "freezing drizzle",""} ,
        new string[]{"9", "drizzle","نم نم باران"} ,
        new string[]{"10", "freezing rain","باران منجمد"} ,
        new string[]{"11", "showers","رگبار"} ,
        new string[]{"12", "showers","رگبار"} ,
        new string[]{"13", "snow flurries","بارش ناگهانی برف"} ,
        new string[]{"14", "light snow showers","بارش برف روشن"} ,
        new string[]{"15", "blowing snow","برف همراه باد"} ,
        new string[]{"16", "snow","برف"} ,
        new string[]{"17", "hail","تگرگ"} ,
        new string[]{"18", "sleet","برف و باران"} ,
        new string[]{"19", "dust","گرد و خاک"} ,
        new string[]{"20", "foggy","مه آلود"} ,
        new string[]{"21", "haze","غبار آلود"} ,
        new string[]{"22", "smoky","پر دود"},
        new string[]{"23", "blustery","پر باد"} ,
        new string[]{"24", "windy","وزش باد"} ,
        new string[]{"25", "cold","سرد"} ,
        new string[]{"26", "cloudy","ابری"} ,
        new string[]{"27", "mostly cloudy (night)","بیشتر قسمتها ابری (شب)"} ,
        new string[]{"28", "mostly cloudy (day)","بیشتر قسمتها ابری (روز)"} ,
        new string[]{"29", "partly cloudy (night)","قسمتی ابری (شب)"} ,
        new string[]{"30", "partly cloudy (day)","قسمتی ابری (روز)"} ,
        new string[]{"31", "clear (night)","صاف (شب)"} ,
        new string[]{"32", "sunny","آفتابی"} ,
        new string[]{"33", "fair (night)","صاف (شب)"} ,
        new string[]{"34", "fair (day)","صاف (روز)"} ,
        new string[]{"35", "mixed rain and hail","باران و تگرگ"} ,
        new string[]{"36", "hot","گرم"} ,
        new string[]{"37", "isolated thunderstorms","رعد و برق جدا شده"} ,
        new string[]{"38", "scattered thunderstorms","رعد و برق پراکنده"} ,
        new string[]{"39", "scattered thunderstorms","رعد و برق پراکنده"} ,
        new string[]{"40", "scattered showers","بارش پراکنده"} ,
        new string[]{"41", "heavy snow","برف سنگین"} ,
        new string[]{"42", "scattered snow showers","بارش پراکنده برف"} ,
        new string[]{"43", "heavy snow","برف سنگین"} ,
        new string[]{"44", "partly cloudy","قسمتی ابری"} ,
        new string[]{"45", "thundershowers","رگبار همراه با رعدوبرق"} ,
        new string[]{"46", "snow showers","بارش برف"} ,
        new string[]{"47", "isolated thundershowers","رگبار طوفانی جداشده"}
    };
        #endregion

        #region Week Array
        string[][] strWeekDays =
    {
        new string[]{"Sa","Sat","Saturday","شنبه"},
        new string[]{"Su","Sun","Sunday","یک شنبه"},
        new string[]{"Mo","Mon","Monday","دو شنبه"},
        new string[]{"Tu","Tue","Tuesday","سه شنبه"},
        new string[]{"We","Wed","Wednesday","چهار شنبه"},
        new string[]{"Th","Thu","Thursday","پنج شنبه"},
        new string[]{"Fr","Fri","Friday","جمعه"}
    };
        #endregion

        #region Direction Array
        string[][] strDirection = 
    {
        new string[]{"N","North","شمال"},
        new string[]{"E","East","شرق"},
        new string[]{"S","South","جنوب"},
        new string[]{"W","West","غرب"},
        new string[]{"NE","Northeast","شمال شرق"},
        new string[]{"SE","Southeast","جنوب شرق"},
        new string[]{"SW","Southwest","جنوب غرب"},
        new string[]{"NW","Northwest","شمال غرب"},
        new string[]{"NNE","NorthNorthEast","شمال شمال شرق"},
        new string[]{"ENE","EastNorthEast","شرق شمال شرق"},
        new string[]{"ESE","EastSouthEast","شرق جنوب شرق"},
        new string[]{"SSE","SouthSouthEast","جنوب جنوب شرق"},
        new string[]{"SSW","SouthNorthWest","جنوب جنوب غرب"},
        new string[]{"WSW","WestSouthWest","غرب جنوب غرب"},
        new string[]{"WNW","WestSouthWest","غرب جنوب غرب"},
        new string[]{"NNW","NorthNorthWest","شمال شمال غرب"}
    };
        #endregion

        public Translate()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string FaWeatherByCode(string Code)
        {
            string rtnValue = null;
            for (int i = 0; i < strEnWeatherTypes.Length; i++)
            {
                if (strEnWeatherTypes[i][0] == Code)
                {
                    rtnValue = strEnWeatherTypes[i][2];
                    break;
                }
            }
            return rtnValue;
        }

        public string FaWeatherByType(string EnType)
        {
            string rtnValue = null;
            for (int i = 0; i < strEnWeatherTypes.Length; i++)
            {
                if (strEnWeatherTypes[i][1].Equals(EnType))
                {
                    rtnValue = strEnWeatherTypes[i][2];
                    break;
                }
            }
            return rtnValue;
        }

        public string FaWeekDaysByDayName(string EnDayName)
        {
            string rtnValue = null;
            for (int i = 0; i < strWeekDays.Length; i++)
            {
                if (strWeekDays[i][2] == EnDayName)
                {
                    rtnValue = strWeekDays[i][3];
                    break;
                }
            }
            return rtnValue;
        }

        public string FaWeekDaysByDayShortAbbreviation(string EnDayShortAbbreviation)
        {
            string rtnValue = null;
            for (int i = 0; i < strWeekDays.Length; i++)
            {
                if (strWeekDays[i][0] == EnDayShortAbbreviation)
                {
                    rtnValue = strWeekDays[i][3];
                    break;
                }
            }
            return rtnValue;
        }

        public string FaWeekDaysByDayAbbreviation(string EnDayAbbreviation)
        {
            string rtnValue = null;
            for (int i = 0; i < strWeekDays.Length; i++)
            {
                if (strWeekDays[i][1] == EnDayAbbreviation)
                {
                    rtnValue = strWeekDays[i][3];
                    break;
                }
            }
            return rtnValue;
        }

        public string FaDirectionByName(string EnDirectionName)
        {
            string rtnValue = null;
            for (int i = 0; i < strDirection.Length; i++)
            {
                if (strDirection[i][1] == EnDirectionName)
                {
                    rtnValue = strDirection[i][2];
                    break;
                }
            }
            return rtnValue;
        }

        public string FaDirectionByAbbreviation(string EnDirectionAbbreviation)
        {
            string rtnValue = null;
            for (int i = 0; i < strDirection.Length; i++)
            {
                if (strDirection[i][0] == EnDirectionAbbreviation)
                {
                    rtnValue = strDirection[i][2];
                    break;
                }
            }
            return rtnValue;
        }
    }
}