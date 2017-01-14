﻿using System;
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




    /*
    public long word_count { get; set; }
    public string processed_language { get; set; }
    public IEnumerable<Personalidade> personality { get; set; }
    public IEnumerable<Necessidades>needs { get; set; }
    public IEnumerable<Valores> values { get; set; }
    public IEnumerable<Comportamento> behavior { get; set; }
    public IEnumerable<PreferenciasConsumidor> consumption_preferences { get; set; }
}
public class Personalidade
{
    public string trait_id { get; set; }
    public string name { get; set; }
    public string category { get; set; }
    public long percentile { get; set; }
    public long raw_score { get; set; }
    public IEnumerable<Filho> children { get; set; }

}
public class Filho
{
    public string trait_id { get; set; }
    public string name { get; set; }
    public string category { get; set; }
    public long percentile { get; set; }
    public long raw_score { get; set; }
}
public class Necessidades
{
    public string trait_id { get; set; }
    public string name { get; set; }
    public string category { get; set; }
    public long percentile { get; set; }
    public long raw_score { get; set; }
}
public class Valores
{
    public string trait_id { get; set; }
    public string name { get; set; }
    public string category { get; set; }
    public long percentile { get; set; }
    public long raw_score { get; set; }
}
public class Comportamento
{
    public string trait_id { get; set; }
    public string name { get; set; }
    public string category { get; set; }
    public long percentile { get; set; }
}
public class PreferenciasConsumidor
{
    public string consumption_preference_category_id { get; set; }
    public string name { get; set; }
    public IEnumerable<RankPreferenciasConsumidor> consumption_preferences { get; set; }
}
public class RankPreferenciasConsumidor
{
    public string consumption_preference_category_id { get; set; }
    public string name { get; set; }
    public int score { get; set; }
}
*/

}