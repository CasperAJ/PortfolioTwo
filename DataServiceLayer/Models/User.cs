﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLayer.Models
{
    class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
