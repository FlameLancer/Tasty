using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tasty.Models.ViewModels
{
    public class ChangeUserRolesViewModel
    {
        public string Id { get; set; }
        public SelectList Roles { get; set; }
        public IEnumerable<string> SelectedRoles { get; set; }
    }
}
