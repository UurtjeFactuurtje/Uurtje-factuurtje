﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public enum MenuItemType
    {
        Browse,
        Profile,
        About

    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
