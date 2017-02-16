using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteWatson.Models
{
    public class TreeViewModels
    {
        public TreeViewModels()
        {
            Nodes = new HashSet<TreeViewModels>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Nota { get; set; }
        public ICollection<TreeViewModels> Nodes { get; set; }
    }
}