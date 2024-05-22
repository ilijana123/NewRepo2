using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Models;
using System.Collections.Generic;
namespace Store.ViewModels
{
    public class AvtorViewModel
    {
        public IList<Avtor> Avtori { get; set; }
        public string SearchStringI { get; set; }
        public string SearchStringP { get; set; }
        public string SearchStringN { get; set; }
    }
}