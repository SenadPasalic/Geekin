﻿using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Geekin.ViewModels
{
    public class AddCategoryVM
    {
        //[Display(Name = "Category")]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
