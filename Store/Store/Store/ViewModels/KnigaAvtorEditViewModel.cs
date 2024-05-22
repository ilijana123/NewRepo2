using Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Store.ViewModels
{
    public class KnigaAvtorEditViewModel
    {
        public Avtor avtor { get; set; }
        public IEnumerable<int>? SelectedKnigas { get; set; }
        public IEnumerable<SelectListItem>? KnigaList { get; set; }
    }
}
