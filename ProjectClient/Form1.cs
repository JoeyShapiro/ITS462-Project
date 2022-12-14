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
// for printing to pdf
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Drawing.Layout;

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
                   var text = "";
                foreach (XmlNode element in item)
                {
                    if(choice == "filters")
                    {
                        deviceDetailsBox.Text += element.InnerText + " ";
                        //Console.WriteLine(element.InnerText);
                        text += element.InnerText + " ";
                    }
                    else if(choice == "results")
                    {
                        deviceDetailsBox.Text += element.Name + ": " + element.InnerText + "\r\n";
                        text += element.Name + ": " + element.InnerText + "\r\n";
                    }
                    else
                    {
                        deviceDetailsBox.Text += (element.Name + ": " + element.InnerText + " ");
                        //Console.WriteLine(element.Name + " " + element.InnerText);
                        text += (element.Name + ": " + element.InnerText + " ");
                    }
                }
                deviceDetailsBox.Text += "\r\n";
                list.Add(text + "\r\n\r\n");
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
                list.Add(this_line);
            }

            return list;
        }

        public async Task<List<string>> getFiltersAsList()

        {
            try
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
            catch(Exception ex)
            {
                deviceDetailsBox.Text = "Error connecting to the API";
                return new List<string>();
            }
        }

        public async Task<List<string>> getDevicesAsList()

        {
            try
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
            catch(Exception ex)
            {
                deviceDetailsBox.Text = "Error connecting to the API";
                return new List<string>();
            }
        }

        public async Task<List<string>> getDetailedDevices(string id)
        //public async void getDetailedDevices(string id)
        {
            try
            {
                Convert.ToInt32(id);
            }
            catch
            {
                deviceDetailsBox.Text += "ID must be an integer";
            }
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5206/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Project/GetDeviceDetails?id=" + id);
                string xml_string = "";
                xml_string = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    List<string> xml_list = xmlToList(xml_string, "results");
                    //xmlToList(xml_string, "results");
                    return xml_list;
                }
                else
                {
                    deviceDetailsBox.Text = "Error in response from API";
                    return new List<string>();
                }
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
            var message = "";
            var caption = "Error";
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
                    message = await response.Content.ReadAsStringAsync();
                    caption = "Success";
                }
                else
                {
                    deviceDetailsBox.Text = "Error scraping the data";
                    message = "Error scraping the data";
                }
            }catch(Exception ex)
            {
                deviceDetailsBox.Text = "Error connecting to the API: " + ex.Message;
                message = "Error connecting to the API: " + ex.Message;
            }
            MessageBox.Show(message, caption);
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
            var item = filterBox.SelectedItem ?? "";
            if (item.ToString() == "")
            {
                MessageBox.Show("Please Select a Filter", "Error");
                return;
            }
            _ = getFilteredDevices(item.ToString(), searchBox.Text);
        }

        private async void btnPDF_Click(object sender, EventArgs e)
        {
            //Create PDF Document
            var document = new PdfDocument();

            foreach (var device_string in filterResultsBox.Items.Cast<string>().ToList())
            {
                var id = device_string.Split(' ')[1];

                var device = await getDetailedDevices(id);

                // set the apge parameters
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);
                var font = new XFont("Verdana", 12, XFontStyle.Bold);

                // draw the stuff
                var tf = new XTextFormatter(gfx);
                tf.DrawString(device[0], font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height));
            }

            //Specify file name of the PDF file
            string filename = "results.pdf";
            //Save PDF File
            document.Save(filename);
            //Load PDF File for viewing
            Process.Start(filename);

            MessageBox.Show("Saved detailed list to \"results.pdf\"", "Success");
        }
    }
}
