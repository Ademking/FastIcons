using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FastIcon
{
    class Icons
    {
        public string search_query { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public dynamic records { get; set; }
        
        public List<string> big_ids { get; set; }

        public List<string> urls_list { get; set; }
        public Icons (string search)
        {
            this.search_query = search;
            big_ids = new List<string>();
            urls_list = new List<string>();
        }

        public void Download_json()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("https://www.flaticon.com/ajax/autocomplete/" + search_query);


                string first_string_of_json = json.Substring(0, 1);

                if (first_string_of_json == "[")
                {
                    this.records = JsonConvert.DeserializeObject<List<Welcome>>(json);
                    Console.WriteLine(records);


                    foreach (var item in records)
                    {
                        foreach (var x in item.Items)
                        {
                            big_ids.Add(x.Id.ToString());
                        }
                    }

                }

                if (first_string_of_json == "{")
                {
                  
                    this.records = JsonConvert.DeserializeObject<Dictionary<string, Welcome>>(json);

                    Console.WriteLine(records);

                    foreach (var item in records)
                    {


                        foreach (var x in item.Value.Items)
                        {
                            big_ids.Add(x.Id.ToString());
                        }
                        /*foreach (var x in item.items)
                           {

                                 big_ids.Add(x.id.ToString());
                           }*/
                    }
                }

                
            }

           
        }



        public void make_url()
        {
            foreach (var item in big_ids)
            {
                string big_id  = item.ToString();
                string mini_id = mini_id_for_url(item.ToString());

                //image0.flaticon.com/icons/png/128/2/2772.png
                string url = "https://image0.flaticon.com/icons/png/512/" + mini_id + "/" + big_id + ".png";

                urls_list.Add(url);
                //Console.WriteLine("id : " + item.ToString() + "---- Mini : " + mini_id_for_url(item.ToString()));
            }
        }



        public string mini_id_for_url(string id)
        {
            if (id.Length <= 3)
            {
                int length = id.Length;
                int length_of_needed_mini_id = 1;
                string mini_id = "0";
               // mini_id = id.Substring(0, length_of_needed_mini_id);
                return mini_id;
            }

           
            else
            {
            int length = id.Length;
                int length_of_needed_mini_id = length - 3;
                string mini_id = "";
                mini_id = id.Substring(0, length_of_needed_mini_id);
                return mini_id;
            }
           

        }




        /* ============ [ Deserializing JSON Object ] ============*/
        public partial class Welcome
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("tag")]
            public string Tag { get; set; }

            [JsonProperty("icons")]
            public string Icons { get; set; }

            [JsonProperty("items")]
            public List<Item> Items { get; set; }
        }

        public partial class Item
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("team_id")]
            public string TeamId { get; set; }
        }


    }
}
