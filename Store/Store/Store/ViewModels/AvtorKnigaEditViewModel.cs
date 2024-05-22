using Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Store.ViewModels
{
    public class AvtorKnigaEditViewModel
    {
        public Kniga kniga { get; set; }
        public IEnumerable<int>? SelectedAvtors { get; set; }
        public IEnumerable<SelectListItem>? AvtorList { get; set; }
    }
}
