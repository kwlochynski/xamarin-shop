using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WlochynskiKamil.Models
{
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CheckPassword { get; set; }
    }
}