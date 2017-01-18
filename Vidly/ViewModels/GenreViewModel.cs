using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class GenreViewModel
    {
        readonly List<Genre> genres;

        [Display(Name= "Genres")]
        public int SelectedGenreId { get; set; }

        public string SelectedGenre
        {
            get { return this.genres[this.SelectedGenreId].GenreType; }
        }

        public IEnumerable<SelectListItem> GenreItems
        {
            get { return new SelectList(genres, "Id", "GenreType"); }
        }

        public GenreViewModel(List<Genre> genres)
        {
            this.genres = genres;
        }
    }
}