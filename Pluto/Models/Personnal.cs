using System;
using System.Collections.Generic;


namespace Pluto.Models
{
    public class Personnal :User
    {
        public List<Note> notes { get; set; }
    }
}
