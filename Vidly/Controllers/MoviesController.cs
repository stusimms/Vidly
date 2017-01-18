using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;


        //create a DbContext to access our database
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //a DbContext is a disposable object (the link to the database needs to be closed)
        //there is a method to dispose of such objects that we need to overide to ensure that the DbContext we created is disposed of
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Movies
        public ActionResult Random()
        {

            var movie = new Movie() {Name = "Shrek!"};
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
            //return Content("Hello World");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home",new {page = 1, sortBy = "name"});
        }

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }

        public ViewResult Index()
        {
            //get data from Movies table
            //because the Movies table is linked to the Genres table, that linked data also needs to be loaded and therefore have to use the .Include() method
            //the .ToList() method ensures that the query is run at this point
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        //following line is to create an attribute route
        //contraints start with a colon and follow the parameter, for more than one start with a further colon
        //regex is a method that allows a regular expression
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult MovieForm()
        {
            return View();
        }

        //public ActionResult MoviesInStock(int id)
        //{
            //var genre = new Genre
            //{
                //Id = id,
                //GenreType = "all"
            //};
            //var viewModel = new GenreListViewModel();
            ////viewModel.Genres.Add(genre);

            //return View(viewModel);
        //}

        
        //Method works to display report passing values of Genres that can be selected 
        public ActionResult MoviesInStock(int Id)
        {
            var viewModel = new GenreListViewModel();
            //{
                //Genres = _context.Genres.ToList()
            //};
            viewModel.Genres = _context.Genres.ToList();

            return View(viewModel);
        }

        public ActionResult SelectGenre(int Id)
        {
            //var genre = new Genre
            //{
                //Id = id,
                //GenreType = _context.Genres.
            //};
            var viewModel = new GenreListViewModel();
            //viewModel.Genres.Add(selectedGenre);

            return RedirectToAction("MoviesInStock", "Movies", Id);
        }

    }

}