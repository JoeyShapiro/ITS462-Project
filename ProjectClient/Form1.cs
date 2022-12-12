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
                        textBox1.Text += element.InnerText + " ";
                        //Console.WriteLine(element.InnerText);
                    }
                    else
                    {
                        textBox1.Text += (element.Name + " " + element.InnerText + " ");
                        //Console.WriteLine(element.Name + " " + element.InnerText);
                    }
                }
                textBox1.Text += "\r\n";
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
            List<string> xml_list = xmlToList(xml_string, "devices");
            return xml_list;
        }

        public async Task<List<string>> getDetailedDevices(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5206/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("api/Project/GetDeviceDetails?id="+id);
            string xml_string = "";
            xml_string = await response.Content.ReadAsStringAsync();
            List<string> xml_list = xmlToList(xml_string, "results");
            return xml_list;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString() == "Detailed Devices")
            {
                textBox1.Text = "";
                _ = getDetailedDevices(textBox2.Text);
            }
            else if(comboBox1.SelectedItem.ToString() == "Filters")
            {
                textBox1.Text = "";
                _ = getFiltersAsList();
            }
            else if (comboBox1.SelectedItem.ToString() == "Devices")
            {
                textBox1.Text = "";
                _ = getDevicesAsList();
            }

        }
    }
}
