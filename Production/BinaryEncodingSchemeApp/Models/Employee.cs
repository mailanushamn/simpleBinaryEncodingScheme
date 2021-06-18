using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingApp.Models
{
    [Serializable]
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
    }
}
