using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace Pluto.Models
{
    public class Parent : User
    {
		public string professionParent { get; set; }
		public string typeParent { get; set; }
		public string relationTuteur { get; set; }		
        private List<Ass_parent_student> ass_parent_students { get; set; }



    }
}