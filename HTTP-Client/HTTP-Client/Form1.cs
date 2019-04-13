using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms;

namespace HTTP_Client
{
    public partial class Form1 : Form
    {
        const string hr = "\r\n===================================\r\n";
        public Form1()
        {
            InitializeComponent();
            methodsComBox.Items.Add(HttpMethod.Get);
            methodsComBox.Items.Add(HttpMethod.Post);
            methodsComBox.Items.Add(HttpMethod.Head);
            methodsComBox.Items.Add(HttpMethod.Delete);
            methodsComBox.Items.Add(HttpMethod.Put);
            methodsComBox.SelectedIndex = 0;
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            SendRequestAsync();
        }
        private async void SendRequestAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string uriTextbox = "http://www.google.com/";
                    HttpRequestMessage request = new HttpRequestMessage(methodsComBox.SelectedItem as HttpMethod, uriTextbox)
                    {
                        Version = Version.Parse("1.0"),
                    };
                    textBox1.Text += $"{request.Method} {request.RequestUri} HTTP/{request.Version}{hr}";
                    HttpResponseMessage response = await client.SendAsync(request);
                    textBox1.Text += $"HTTP/{response.Version} {response.StatusCode}{hr}";
                    textBox1.Text += response.Headers + hr;
                    textBox1.Text += await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
        }
    }
}
