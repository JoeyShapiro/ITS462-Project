using MySql.Data.MySqlClient;
using System.Numerics;
using System.Xml;

namespace ProjectServerRestful.Services
{
	public class Database
	{
		// this is used by both filtered and unfiltered
		public static string GetDevicesFromDBAsXML(string stmt)
		{
			var conn = connect();
			var reader = query(conn, stmt);

			XmlDocument doc = new XmlDocument();
			XmlNode results = doc.CreateElement("results");

			XmlNode devices = doc.CreateElement("devices");

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					// create a new drone using the row from the reader
					XmlNode drone = newDevice(doc,
						reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2)
					);

					// finish it
					devices.AppendChild(drone);
				}
			}

			results.AppendChild(devices);
			doc.AppendChild(results);

			reader.Close();
			conn.Close();

			return doc.OuterXml;
		}

		// this is to get details of a device
		public static string GetDeviceDetailsFromDBAsXML(string stmt)
		{
            var conn = connect();
            var reader = query(conn, stmt);

            XmlDocument doc = new XmlDocument();
            XmlNode results = doc.CreateElement("results");

			if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // create a new device using the row from the reader
                    XmlNode device = newDeviceDetailed(doc,
                        reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDouble(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)
                    );

                    // finish it
                    results.AppendChild(device);
                }
            }

            doc.AppendChild(results);

            reader.Close();
			conn.Close();

            return doc.OuterXml;
        }

		public static string GetDeviceFiltersFromDBAsXML()
		{
            var conn = connect();
            var reader = query(conn, "CALL list_filters();");

            XmlDocument doc = new XmlDocument();
            XmlNode results = doc.CreateElement("results");
			XmlNode filters = doc.CreateElement("filters");

			if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // create a node for the filter
                    XmlNode filter = doc.CreateElement("filter");
					filter.InnerText = reader.GetString(0);

                    // finish it
                    filters.AppendChild(filter);
				}
			}

			results.AppendChild(filters);
			doc.AppendChild(results);

            reader.Close();
			conn.Close();

            return doc.OuterXml;
        }

		public static int AddDevice(string computer_type, string vendor, string model, double price, string link, string description, string specs)
		{
            var conn = connect();
			var stmt = string.Format("CALL add_device('{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}');", computer_type, vendor, model, price, link, description.Replace("'", "\\'"), specs);
            var reader = query(conn, stmt);

			var result = reader.RecordsAffected;

            reader.Close();
			conn.Close();

			return result;
        }

		private static MySqlDataReader query(MySqlConnection conn, string stmt)
		{
			var cmd = new MySqlCommand(stmt, conn);
			var data_reader = cmd.ExecuteReader();

			return data_reader;
		}

		private static MySqlConnection connect()
		{
			const string connection_string = @"Server=localhost;Port=9906;Uid=user;Pwd=password;Database=project";
			var conn = new MySqlConnection(connection_string);
			conn.Open();

			return conn;
		}

		private static XmlNode newDevice(XmlDocument doc, int id, string model, double price)
		{
			// create nodes
			XmlNode device = doc.CreateElement("device");

			XmlNode nId = doc.CreateElement("id");
			XmlNode nModel = doc.CreateElement("model");
			XmlNode nPrice = doc.CreateElement("price");

			// set values
			nId.InnerText = id.ToString();
			nModel.InnerText = model;
			nPrice.InnerText = price.ToString();

			device.AppendChild(nId);
			device.AppendChild(nModel);
			device.AppendChild(nPrice);
			return device;
		}

		private static XmlNode newDeviceDetailed(XmlDocument doc, int id, string computer_type, string vendor, string model, double price, string link, string description, string specs)
		{
            // create nodes
            XmlNode device = doc.CreateElement("device");

            XmlNode nId = doc.CreateElement("id");
			XmlNode nType = doc.CreateElement("computer_type");
            XmlNode nvendor = doc.CreateElement("vendor");
            XmlNode nModel = doc.CreateElement("model");
            XmlNode nPrice = doc.CreateElement("price");
            XmlNode nLink = doc.CreateElement("link");
            XmlNode nDesc = doc.CreateElement("description");
            XmlNode nSpec = doc.CreateElement("specs");

            // set values
            nId.InnerText = id.ToString();
			nType.InnerText = computer_type;
			nvendor.InnerText = vendor;
            nModel.InnerText = model;
            nPrice.InnerText = price.ToString();
			nLink.InnerText = link;
			nDesc.InnerText = description;
			nSpec.InnerText = specs;

            device.AppendChild(nId);
			device.AppendChild(nType);
			device.AppendChild(nvendor);
			device.AppendChild(nModel);
            device.AppendChild(nPrice);
			device.AppendChild(nLink);
			device.AppendChild(nDesc);
			device.AppendChild(nSpec);

			return device;
        }
	}
}
