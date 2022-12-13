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
			// connect and query
			var conn = connect();
			var reader = query(conn, stmt);

			// create the doc
			XmlDocument doc = new XmlDocument();
			XmlNode results = doc.CreateElement("results");

			XmlNode devices = doc.CreateElement("devices");

			// start reading rows
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					// create a new device using the row from the reader
					XmlNode device = newDevice(doc,
						reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2)
					);

					// finish it
					devices.AppendChild(device);
				}
			}

			// add to xml
			results.AppendChild(devices);
			doc.AppendChild(results);

			// close everything
			reader.Close();
			conn.Close();

			return doc.OuterXml;
		}

		// this is to get details of a device
		public static string GetDeviceDetailsFromDBAsXML(string stmt)
		{
			// connect to db and use statement
            var conn = connect();
            var reader = query(conn, stmt);

			// start xml structure (response)
            XmlDocument doc = new XmlDocument();
            XmlNode results = doc.CreateElement("results");

			// start reading the data
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

			// add to xml
            doc.AppendChild(results);

			// close readers
            reader.Close();
			conn.Close();

            return doc.OuterXml;
        }

		// gets all the filters
		public static string GetDeviceFiltersFromDBAsXML()
		{
			// connect and use procedure
            var conn = connect();
            var reader = query(conn, "CALL list_filters();");

			// create response structure
            XmlDocument doc = new XmlDocument();
            XmlNode results = doc.CreateElement("results");
			XmlNode filters = doc.CreateElement("filters");

			// start reading data
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

			// add to filter
			results.AppendChild(filters);
			doc.AppendChild(results);

			// close readers
            reader.Close();
			conn.Close();

            return doc.OuterXml;
        }

		// add a device to the db
		public static int AddDevice(string computer_type, string vendor, string model, double price, string link, string description, string specs)
		{
			// connect to db
            var conn = connect();
			// create the description, if it is too long for db trim it
			var desc = description.Length > 1024 ? Truncate(description, 1024) : description;
			// execute statement
			var stmt = string.Format("CALL add_device('{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}');", computer_type, vendor, model, price, link, desc.Replace("\'", "\\'"), specs);
            var reader = query(conn, stmt);

			// get how many records affected
			var result = reader.RecordsAffected;

			// close everything
            reader.Close();
			conn.Close();

			return result; // returns how many where changed/added (should be 1)
        }

		// remove excess characters
        private static string Truncate(string value, int maxLength, string truncationSuffix = "...")
        {
			// the "-3" is for the "..."
			return value.Substring(maxLength - 3) + truncationSuffix;
        }

		// remove all data from db and set pk back to 0
        public static void CleanDB()
		{
			var conn = connect();
			var stmt = "truncate devices";
			var reader = query(conn, stmt);

			reader.Close();
			conn.Close();
		}

		// query the db
		private static MySqlDataReader query(MySqlConnection conn, string stmt)
		{
			var cmd = new MySqlCommand(stmt, conn);
			var data_reader = cmd.ExecuteReader();

			return data_reader;
		}

		// connectes to the database
		private static MySqlConnection connect()
		{
			const string connection_string = @"Server=localhost;Port=9906;Uid=user;Pwd=password;Database=project";
			var conn = new MySqlConnection(connection_string);
			conn.Open();

			return conn;
		}

		// creates a new xml node for a device
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

		// creates a new detailed device
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
