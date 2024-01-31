﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Users
    {
        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public Users()
        {
            this.username = "";
            this.name = "";
            this.email = "";
            this.password = "";
        }
    }
}