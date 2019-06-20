using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace praticeApp.Domain
{


    public class BeaconJSON
    {
        public class BeaconJSONEntry
        {

            [JsonProperty("id")]
            public String UUID { get; set; }
            [JsonProperty("strength")]
            public short RSSI { get; set; }
        }

        [JsonProperty("beacons")]
        private Dictionary<String, List<BeaconJSONEntry>> payload;

        public BeaconJSON() => payload = new Dictionary<String, List<BeaconJSONEntry>>();

        public void Add(String UUID, short RSSI)
        {
            if (UUID.Length == 0)
                return;

            BeaconJSONEntry entry = new BeaconJSONEntry();

            entry.RSSI = RSSI;
            entry.UUID = UUID;

            if (!payload.ContainsKey("beacons")) {
                payload["beacons"] = new List<BeaconJSONEntry>();
            }

            payload["beacons"].Add(entry);
        }

        public String ParseJson()
        {
            return JsonConvert.SerializeObject(payload, Formatting.Indented);
        }
    }
}
