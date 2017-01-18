using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteWatson.Models
{
    public class RetornoApiPerfil
    {
        public int word_count { get; set; }
        public string processed_language { get; set; }
        public List<Personality> personality { get; set; }
        public List<Need> needs { get; set; }
        public List<Value> values { get; set; }
        public List<Behavior> behavior { get; set; }
        public List<ConsumptionPreference> consumption_preferences { get; set; }
        public List<object> warnings { get; set; }
    }
    public class Child
    {
        public string trait_id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double percentile { get; set; }
        public double raw_score { get; set; }
    }

    public class Personality
    {
        public string trait_id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double percentile { get; set; }
        public double raw_score { get; set; }
        public List<Child> children { get; set; }
    }

    public class Need
    {
        public string trait_id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double percentile { get; set; }
        public double raw_score { get; set; }
    }

    public class Value
    {
        public string trait_id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double percentile { get; set; }
        public double raw_score { get; set; }
    }

    public class Behavior
    {
        public string trait_id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double percentage { get; set; }
    }

    public class ConsumptionPreference2
    {
        public string consumption_preference_id { get; set; }
        public string name { get; set; }
        public double score { get; set; }
    }

    public class ConsumptionPreference
    {
        public string consumption_preference_category_id { get; set; }
        public string name { get; set; }
        public List<ConsumptionPreference2> consumption_preferences { get; set; }
    }



public class JSONInputAnalise
    {
        public List<ContentItems> contentItems { get; set; }

        public JSONInputAnalise()
        {
            this.contentItems = new List<ContentItems>();
        }
    }
    public class ContentItems
    {
        public string contenttype { get; set; }
        public string language { get; set; }
        public string id { get; set; }
        public string content { get; set; }
    }

}