using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace TesteWatson.Controllers
{
    public class TeacherController : Controller
    {
        [HandleError]
        public class HomeController : Controller
        {
            public ActionResult Index()
            {
                ViewData["Message"] = "TreeView Demo";

                return View();
            }

            public ActionResult About()
            {
                return View();
            }

            [AcceptVerbs(HttpVerbs.Post)]
            public string GetTreeData(string id)
            {
                List<AjaxTreeNodeJsonObject> results = new List<AjaxTreeNodeJsonObject>();

                //NODE ID: TREEID_LEVEL_PARENTID_ID

                //this should be the first call from the tree to initialize

                if (string.IsNullOrEmpty(id) | id == "0")
                {
                    results.Add(new AjaxTreeNodeJsonObject("TREE1_0_0_1", "U.S.A.", "#", true));
                    results.Add(new AjaxTreeNodeJsonObject("TREE1_0_0_2", "Canada", "#", true));


                }
                else
                {
                    //Get Child Nodes

                    //these are subsequent calls to populate the tree nodes
                    string[] nodeInfo = id.Split('_');
                    string nodeTree = nodeInfo[0];
                    int nodeLevel = Convert.ToInt32(nodeInfo[1]);
                    int nodeParentId = Convert.ToInt32(nodeInfo[2]);
                    int nodeId = Convert.ToInt32(nodeInfo[3]);

                    //this would be better as a database lookup or some hierarchical tree structure

                    switch (nodeTree)
                    {
                        //WHICH TREE?

                        case "TREE1":

                            switch (nodeLevel)
                            {
                                //WHICH LEVEL?

                                case 0:
                                    //country

                                    switch (nodeParentId)
                                    {
                                        //WHICH PARENT?

                                        case 0:
                                            //no parent

                                            switch (nodeId)
                                            {
                                                //WHICH NODE?

                                                case 1:
                                                    //USA

                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_1", "Alabama", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_2", "Alaska", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_3", "Arizona", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_4", "Arkansas", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_5", "California", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_6", "Colorado", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_7", "Connecticut", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_8", "Delaware", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_9", "Florida", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_10", "Georgia", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_11", "Hawaii", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_12", "Idaho", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_13", "Illinois", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_14", "Indiana", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_15", "Iowa", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_16", "Kansas", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_17", "Kentucky", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_18", "Louisiana", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_19", "Maine", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_20", "Maryland", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_21", "Massachusetts", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_22", "Michigan", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_23", "Minnesota", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_24", "Mississippi", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_25", "Missouri", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_26", "Montana", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_27", "Nebraska", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_28", "Nevada", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_29", "New Hampshire", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_30", "New Jersey", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_31", "New Mexico", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_32", "New York", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_33", "North Carolina", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_34", "North Dakota", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_35", "Ohio", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_36", "Oklahoma", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_37", "Oregon", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_38", "Pennsylvania", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_39", "Rhode Island", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_40", "South Carolina", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_41", "South Dakota", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_42", "Tennessee", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_43", "Texas", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_44", "Utah", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_45", "Vermont", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_46", "Virginia", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_47", "Washington", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_48", "West Virginia", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_49", "Wisconsin", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1_50", "Wyoming", "#"));

                                                    break;
                                                case 2:
                                                    //CANDA

                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_1", "Ontario", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_2", "Quebec", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_3", "Nova Scotia", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_4", "New Brunswick", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_5", "Manitoba", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_6", "British Columbia", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_7", "Prince Edward Island", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_8", "Saskatchewan", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_9", "Alberta", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_10", "Newfoundland and Labrador", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_11", "Northwest Territories", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_12", "Yukon", "#"));
                                                    results.Add(new AjaxTreeNodeJsonObject("TREE1_1_13", "Nunavut", "#"));

                                                    break;
                                            }

                                            break;
                                    }

                                    break;
                            }

                            break;
                    }

                }

                JavaScriptSerializer ser = new JavaScriptSerializer();

                return ser.Serialize(results);
            }

        }


        internal class AjaxTreeNodeJsonObject
        {

            private AjaxTreeNodeJsonObjectAttribute _attr = new AjaxTreeNodeJsonObjectAttribute();
            private string _state = string.Empty;
            private string _icon = string.Empty;
            private string _metadata = string.Empty;
            private AjaxTreeNodeJsonObjectDataLinkAttribute _data = new AjaxTreeNodeJsonObjectDataLinkAttribute();

            private AjaxTreeNodeJsonObject[] _children;

            public AjaxTreeNodeJsonObjectAttribute attr
            {
                get { return _attr; }
            }

            public AjaxTreeNodeJsonObjectDataLinkAttribute data
            {
                get { return _data; }
            }

            public string state
            {
                get { return _state; }
                set { _state = value; }
            }

            public string icon
            {
                get { return _icon; }
                set { _icon = value; }
            }

            public string metadata
            {
                get { return _metadata; }
                set { _metadata = value; }
            }

            public AjaxTreeNodeJsonObject[] children
            {
                get { return _children; }
                set { _children = value; }
            }

            public AjaxTreeNodeJsonObject()
            {
            }

            public AjaxTreeNodeJsonObject(string id, string title, string href)
                : this(id, title, href, false)
            {
            }


            public AjaxTreeNodeJsonObject(string id, string title, string href, bool hasChildren)
            {
                //li values
                this.attr.id = id;
                this.attr.title = title;
                //a values
                this.data.attr.href = href;
                this.data.attr.id = "a" + this.attr.id;
                this.data.title = this.attr.title;

                if (hasChildren)
                {
                    this.state = "closed";
                }

            }


            public AjaxTreeNodeJsonObject(string id, string title, string href, AjaxTreeNodeJsonObject[] children)
            {
                //li values
                this.attr.id = id;
                this.attr.title = title;
                //a values
                this.data.attr.href = href;
                this.data.attr.id = "a" + this.attr.id;
                this.data.title = this.attr.title;

                if (children != null && children.Length > 0)
                {
                    this.state = "closed";
                    this.children = children;
                }

            }

        }

        internal class AjaxTreeNodeJsonObjectAttribute
        {
            private string _id = string.Empty;

            private string _title = string.Empty;
            /// <summary>
            /// Gets or sets the Id value
            /// </summary>
            public string id
            {
                get { return this._id; }
                set { this._id = value; }
            }

            /// <summary>
            /// Gets or sets the Title value
            /// </summary>
            public string title
            {
                get { return this._title; }
                set { this._title = value; }
            }

        }

        internal class AjaxTreeNodeJsonObjectDataLinkAttribute
        {

            private string _title = string.Empty;

            private AjaxTreeNodeJsonObjectLinkAttribute _attr = new AjaxTreeNodeJsonObjectLinkAttribute();
            /// <summary>
            /// Gets or sets the Title value
            /// </summary>
            public string title
            {
                get { return this._title; }
                set { this._title = value; }
            }

            public AjaxTreeNodeJsonObjectLinkAttribute attr
            {
                get { return _attr; }
                set { _attr = value; }
            }

        }

        internal class AjaxTreeNodeJsonObjectLinkAttribute
        {
            private string _id = string.Empty;
            private string _href = string.Empty;

            private string _target = string.Empty;
            /// <summary>
            /// Gets or sets the Id value
            /// </summary>
            public string id
            {
                get { return this._id; }
                set { this._id = value; }
            }

            /// <summary>
            /// Gets or sets the Href value
            /// </summary>
            public string href
            {
                get { return this._href; }
                set { this._href = value; }
            }

            /// <summary>
            /// Gets or sets the Target value
            /// </summary>
            public string target
            {
                get { return this._target; }
                set { this._target = value; }
            }

        }
        }
}
