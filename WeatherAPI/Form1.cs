using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using TemperatorConvertService;

namespace WeatherAPI
{
    public partial class Form1 : Form
    {
        const string APPID = "745eaa220b2a71c1d3bedba10a6e0064";
        string cityName = "Kitchener";
        string currentUnit = "";
        const string CELSIUS = "Celsius";
        const string FAHRENHEIT = "Fahrenheit";
        WeatherInfo.root outPut;

        public Form1()
        {
            InitializeComponent();

            label1.Hide();
            label2.Hide();
            label3.Hide();
            lbl_cityName.Hide();
            lbl_country.Hide();
            lbl_Temp.Hide();
            button1.Hide();
            
        }

        void getWeather(string city)
        {

            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0},CA&appid={1}&units=metric&cnt=6", city, APPID);
                try
                {
                    var json = web.DownloadString(url);

                    var result = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                    outPut = result;

                    lbl_cityName.Text = string.Format("{0}", outPut.name);
                    lbl_country.Text = string.Format("{0}", outPut.sys.country);
                    lbl_Temp.Text = string.Format("{0} \u00B0" + "C", outPut.main.temp);
                    button1.Text = "Convert to " + FAHRENHEIT;
                    currentUnit = CELSIUS;
                    showDetails();
                }
                catch (Exception)
                {
                    MessageBox.Show("City name does not exist in Canada");
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var cityInput = txtCity.Text;
            getWeather(cityInput);
        }

        private void showDetails()
        {
            label1.Show();
            label2.Show();
            label3.Show();
            lbl_cityName.Show();
            lbl_country.Show();
            lbl_Temp.Show();
            button1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Service1 s = new Service1();

            if (currentUnit != FAHRENHEIT)
            {
                button1.Text = "Convert to " + CELSIUS;
                lbl_Temp.Text = string.Format("{0} \u00B0" + "F", s.ToFahrenheit(outPut.main.temp));
                currentUnit = FAHRENHEIT;
            } else
            {
                button1.Text = "Convert to " + FAHRENHEIT;
                lbl_Temp.Text = string.Format("{0} \u00B0" + "C", outPut.main.temp);
                currentUnit = CELSIUS;
            }
            
        }
    }
}
