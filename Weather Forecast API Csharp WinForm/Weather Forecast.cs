using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.IO;
using Weather_Forecast_API_Csharp_WinForm.Onecall_API;

namespace Weather_Forecast_API_Csharp_WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != String.Empty)
            {

                WebClient client = new WebClient();

                client.QueryString.Add("q", comboBox1.Text);
                client.QueryString.Add("APPID", "41cdb41d9e7123e9985071b576dbd277");


                try
                {
                    string str = client.DownloadString("https://api.openweathermap.org/data/2.5/weather");
                    dynamic weather = JsonConvert.DeserializeObject(str);
                    client.QueryString.Clear();
                    //https://api.openweathermap.org/data/2.5/onecall?lat=40.3777&lon=49.892&APPID=41cdb41d9e7123e9985071b576dbd277
                    client.QueryString.Add("lat", weather.coord.lat.ToString());
                    client.QueryString.Add("lon", weather.coord.lon.ToString());
                    client.QueryString.Add("APPID", "41cdb41d9e7123e9985071b576dbd277");
                    str = client.DownloadString("https://api.openweathermap.org/data/2.5/onecall");
                    OneCall oneCall = JsonConvert.DeserializeObject<OneCall>(str);
                    if (!comboBox1.Items.Contains(comboBox1.Text))
                    {
                        comboBox1.Items.Add(comboBox1.Text);
                    }
                    string result;
                    switch ((sender as Button).Text)
                    {
                        case "Show today":
                            result = Info(oneCall, 1);
                            break;
                        case "Show 7 days":
                            result = Info(oneCall, 7);
                            break;
                        default:
                            result = String.Empty;
                            break;
                    }
                    textBox2.Text = result;
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private string Info(OneCall oneCall, int days)
        {
            if (days > oneCall.daily.Count)
            {
                days = oneCall.daily.Count;
            }
            else if (days < 0)
            {
                return String.Empty;
            }
            if (oneCall == null)
            {
                return String.Empty;
            }
            string str = "";
            DateTime date;
            for (int i = 0; i < days; i++)
            {
                date = DateTimeOffset.FromUnixTimeSeconds(oneCall.daily[i].dt).Date;
                str += $"-------------------------------------------" +
                    $"Date: {date.Day}.{date.Month}.{date.Year}" +
                    $"\r\n Sunrise: {DateTimeOffset.FromUnixTimeSeconds(oneCall.daily[i].sunrise).TimeOfDay}" +
                    $"\r\n Sunset: {DateTimeOffset.FromUnixTimeSeconds(oneCall.daily[i].sunset).TimeOfDay}" +
                    $"\r\n Temp, Feels like:" +
                    $"\r\n Morning {(float)(oneCall.daily[i].temp.morn - 273.15)}C, {(float)(oneCall.daily[i].feels_like.morn - 273.15)}C" +
                    $"\r\n Daytime {(float)(oneCall.daily[i].temp.day - 273.15)}C, {(float)(oneCall.daily[i].feels_like.day - 273.15)}C" +
                    $"\r\n Evening {(float)(oneCall.daily[i].temp.eve - 273.15)}C, {(float)(oneCall.daily[i].feels_like.eve - 273.15)}C" +
                    $"\r\n Night {(float)(oneCall.daily[i].temp.night - 273.15)}C, {(float)(oneCall.daily[i].feels_like.night - 273.15)}C" +
                    $"\r\n Humidity: {oneCall.daily[i].humidity}%" +
                    $"\r\n Wind speed: {oneCall.daily[i].wind_speed}m/s" +
                    $"\r\n Clouds: {oneCall.daily[i].clouds}%" +
                    $"\r\n Probability of precipitation: {oneCall.daily[i].pop}%\r\n";
            }
            return str;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Films films = new Films();
            this.Visible = false;
            if (films.ShowDialog() == DialogResult.OK)
            {
                this.Visible = true;
                return;
            }
            this.Close();
        }
    }
}
