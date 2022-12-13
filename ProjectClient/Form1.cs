using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace ProjectClient
{
    public partial class Form1 : Form
    {
        private List<string> xmlToList(string xml, string choice)
        {
            List<string> list = new List<string>();

            // load the xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            // get the root element
            XmlNode items = doc.GetElementsByTagName(choice)[0];

            // add each item
            foreach (XmlNode item in items)
            {
                foreach (XmlNode element in item)
                {
                    if(choice == "filters")
                    {
                        deviceDetailsBox.Text += element.InnerText + " ";
                        //Console.WriteLine(element.InnerText);
                    }
                    else if(choice == "results")
                    {
                        deviceDetailsBox.Text += element.Name + ": " + element.InnerText + "\r\n";
                    }
                    else
                    {
                        deviceDetailsBox.Text += (element.Name + ": " + element.InnerText + " ");
                        //Console.WriteLine(element.Name + " " + element.InnerText);
                    }
                }
                deviceDetailsBox.Text += "\r\n";
            }

            return list;
        }

        private List<string> xmlToList2(string xml, string choice)
        {
            filterResultsBox.Items.Clear();
            List<string> list = new List<string>();

            // load the xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            // get the root element
            XmlNode items = doc.GetElementsByTagName(choice)[0];

            // add each item
            foreach (XmlNode item in items)
            {
                string this_line = "";
                foreach (XmlNode element in item)
                {
                    this_line += (element.Name + ": " + element.InnerText + " ");
                    //Console.WriteLine(element.Name + " " + element.InnerText);
                }
                filterResultsBox.Items.Add(this_line);
            }

            return list;
        }

        public async Task<List<string>> getFiltersAsList()

        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5206/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("api/Project/GetFilters");
            string xml_string = "";
            xml_string = await response.Content.ReadAsStringAsync();
            List<string> xml_list = xmlToList(xml_string, "filters");
            xmlToList(xml_string, "filters");
            return xml_list;
        }

        public async Task<List<string>> getDevicesAsList()

        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5206/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("api/Project/GetDevices");
            string xml_string = "";
            xml_string = await response.Content.ReadAsStringAsync();
            List<string> xml_list = xmlToList2(xml_string, "devices");
            //xmlToList(xml_string, "devices");
            return xml_list;
        }

        public async Task<List<string>> getDetailedDevices(string id)
        //public async void getDetailedDevices(string id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5206/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Project/GetDeviceDetails?id=" + id);
                string xml_string = "";
                xml_string = await response.Content.ReadAsStringAsync();
                List<string> xml_list = xmlToList(xml_string, "results");
                //xmlToList(xml_string, "results");
                return xml_list;
            }
            catch(Exception ex)
            {
                deviceDetailsBox.Text = "Error connecting to the API";
                return new List<string>();
            }
        }

        public async Task<List<string>> getFilteredDevices(string filter, string chosen)
        //public async void getDetailedDevices(string id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5206/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Project/GetFilteredDevices?filter=" + filter + "&chosen=" +  chosen);
                string xml_string = "";
                xml_string = await response.Content.ReadAsStringAsync();
                List<string> xml_list = xmlToList2(xml_string, "devices");
                //xmlToList(xml_string, "results");
                return xml_list;
            }
            catch (Exception ex)
            {
                deviceDetailsBox.Text = "Error connecting to the API";
                return new List<string>();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5206/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Project/Scrape?super_secret_passphrase=project");
                if (response.IsSuccessStatusCode)
                {
                    deviceDetailsBox.Text = "Successfully scraped the data";
                }
                else
                {
                    deviceDetailsBox.Text = "Error scraping the data";
                }
            }catch(Exception ex)
            {
                deviceDetailsBox.Text = "Error connecting to the API";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deviceDetailsBox.Text = "";
            _ = getDetailedDevices(idBox.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deviceDetailsBox.Text = "";
            _ = getDevicesAsList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] id_arr = filterResultsBox.SelectedItem.ToString().Split(':');
            string id = id_arr[1].Trim().Split(' ')[0];
            _ = getDetailedDevices(id);
            deviceDetailsBox.Text = "";
        }

        private void getDeviceByFilterBtn_Click(object sender, EventArgs e)
        {
            deviceDetailsBox.Text = "";
            _ = getFilteredDevices(filterBox.SelectedItem.ToString(), searchBox.Text);
        }
    }
}
