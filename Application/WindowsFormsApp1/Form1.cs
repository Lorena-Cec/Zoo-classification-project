using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.BackgroundImage = Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            InitializeComponent();
        }

        public static string feathers = "0";
        public static string milk = "0";
        public static string backbone = "0";
        public static string toothed = "0";
        public static string eggs = "0";
        public static string hair = "0";
        public static string breathes = "0";
        public static string fins = "0";
        public static string tail = "0";
        public static string airborne = "0";
        public static string legs = "0";
        public static string aquatic = "0";
      
        public void button7_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { feathers = "1"; }
            else { feathers = "0"; }
            if (checkBox2.Checked) { milk = "1"; }
            else { milk = "0"; }
            if (checkBox3.Checked) { toothed = "1"; }
            else { toothed = "0"; }
            if (checkBox4.Checked) { backbone = "1"; }
            else { backbone = "0"; }
            if (checkBox5.Checked) { hair = "1"; }
            else { hair = "0"; }
            if (checkBox6.Checked) { eggs = "1"; }
            else { eggs = "0"; }
            if (checkBox8.Checked) { aquatic = "1"; }
            else { aquatic = "0"; }
            if (checkBox9.Checked) { airborne = "1"; }
            else { airborne = "0"; }
            if (checkBox10.Checked) { tail = "1"; }
            else { tail = "0"; }
            if (checkBox11.Checked) { fins = "1"; }
            else { fins = "0"; }
            if (checkBox12.Checked) { breathes = "1"; }
            else { breathes = "0"; }

            if (radioButton1.Checked) { legs = "0"; }
            if (radioButton2.Checked) { legs = "2"; }
            if (radioButton3.Checked) { legs = "4"; }
            if (radioButton4.Checked) { legs = "5"; }
            if (radioButton5.Checked) { legs = "6"; }
            if (radioButton6.Checked) { legs = "8"; }

            InvokeRequestResponseService().Wait();
        }
        static async Task InvokeRequestResponseService()
        {
            var handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                        (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
          
            using (var client = new HttpClient(handler))
            {
                var requestBody = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                Header = new string[] { "Class_Type", "feathers", "milk", "backbone", "toothed", "eggs", "hair", "breathes", "fins", "tail", "airborne", "legs", "aquatic"},
                                Value = new string[,] {  { "", feathers, milk, backbone, toothed, eggs, hair, breathes, fins, tail, airborne, legs, aquatic },  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                const string apiKey = "Lz5bWCg0zubdmb1QbzEyKngpTgVyg44m";
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new Exception("A key should be provided to invoke the endpoint");
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("http://0213add0-b619-4e42-b2b6-e785c1c5ac7e.westeurope.azurecontainer.io/score");

                HttpResponseMessage response = await client.PostAsJsonAsync("", requestBody).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    int start = result.LastIndexOf("\"Scored Labels\": \"") + "\"Scored Labels\": \"".Length + 1;
                    int length = result.IndexOf("\"}]}}") - start;
                    solutionLabel.Text = result.Substring(start, length);
                }
                else
                {
                    Application.Exit();
                }
                
            }
        }
    }
    public class StringTable
    {
        public string[] Header { get; set; }
        public string[,] Value { get; set; }
    }

}
