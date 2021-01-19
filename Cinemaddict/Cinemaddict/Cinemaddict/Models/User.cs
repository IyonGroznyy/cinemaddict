using System;
using System.Collections.Generic;
using System.Text;

namespace Cinemaddict.Models
{
    public class User
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string PhotoUri { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
    }
}
