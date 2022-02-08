using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Weather_Forecast_API_Csharp_WinForm
{
    public partial class Films : Form
    {
        public Films()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != String.Empty)
            {
                WebClient client = new WebClient();

                client.QueryString.Add("t", comboBox1.Text);
                if (comboBox2.Text != String.Empty)
                {
                    client.QueryString.Add("y", comboBox2.Text);
                }
                client.QueryString.Add("plot", "full");
                client.QueryString.Add("apikey", "11306d30");


                try
                {
                    string str = client.DownloadString("http://www.omdbapi.com/");
                    dynamic info = JsonConvert.DeserializeObject(str);
                    if (!comboBox1.Items.Contains(comboBox1.Text))
                    {
                        comboBox1.Items.Add(comboBox1.Text);
                    }
                    if (!comboBox2.Items.Contains(comboBox2.Text) && comboBox2.Text != String.Empty)
                    {
                        comboBox2.Items.Add(comboBox2.Text);
                    }
                    string result = $"Title: {info.Title}\r\n" +
                        $"Year: {info.Year}\r\n" +
                        $"Rated: {info.Rated}\r\n" +
                        $"Released: {info.Released}\r\n" +
                        $"Runtime: {info.Runtime}\r\n" +
                        $"Genre: {info.Genre}\r\n" +
                        $"Director: {info.Director}\r\n" +
                        $"Writer: {info.Writer}\r\n" +
                        $"Actors: {info.Actors}\r\n" +
                        $"Plot: {info.Plot}\r\n" +
                        $"Language: {info.Language}\r\n" +
                        $"Country: {info.Country}\r\n" +
                        $"Awards: {info.Awards}\r\n" +
                        $"Ratings:\r\n";
                    foreach (var item in info.Ratings)
                    {
                        result += $"Source: {item.Source} Value {item.Value}\r\n";
                    }
                    result += $"Box office: {info.BoxOffice}";
                    textBox2.Text = result;
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
