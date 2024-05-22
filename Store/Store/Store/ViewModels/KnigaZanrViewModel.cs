using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Models;
using System.Collections.Generic;
namespace Store.ViewModels
{
    public class KnigaZanrViewModel
    {
        public IList<Kniga> Knigi { get; set; }
        public SelectList Zanrovi { get; set; }
        public string KnigaZanr { get; set; }
        public string SearchStringN { get; set; }
        public string SearchStringG { get; set; }
        public string SearchStringI { get; set; }

    }
}