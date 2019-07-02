using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EnFutureTowerXm
{
    /// <summary>
    /// Use it for device db.  Read db from file in CSV format. The format is next :
    /// ID Type Team ArtifactType PowerValue Cooldown
    /// ID - must be unique and use as key
    /// </summary>
    public class DeviceDatabase
    {


        public Dictionary<string, DeviceTableRow> actorTable;

        public DeviceDatabase(string filename = "actor_table.csv")
        {
            List<string> csv_table = ReadFileFromCSV(filename);
            foreach (var csv_row in csv_table)
            {
                var db_row = new DeviceTableRow(csv_row);
                actorTable.Add(db_row.ID, db_row);
            }
        }

        public DeviceTableRow GetDeviceById(string id )
        {
            if (actorTable.ContainsKey(id))
                return actorTable[id];
            else
                return null; // bydlocode hule
        }

        /// <summary>
        /// Read content of CSV.
        /// </summary>
        /// <param name="filename">filename or path in user personal folder</param>
        /// <returns>array of readed string </returns>

        private List<string> ReadFileFromCSV(string filename)
        {
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
            var result = new List<string>();
            if (backingFile == null || !File.Exists(backingFile))
            {
                return result;
            }

            
            using (var reader = new StreamReader(backingFile, true))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}