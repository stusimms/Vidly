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
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        //create a DbContext to access our database
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //a DbContext is a disposable object (the link to the database needs to be closed)
        //there is a method to dispose of such objects that we need to overide to ensure that the DbContext we created is disposed of
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult New()
        {
            //create a list of the values of Membership Types
            var membershipTypes = _context.MembershipTypes.ToList();
            //create a CustomerFormViewModel that contains the membership types (so they can be selected when the user creates a new customer)
            //this CustomerFormViewModel will be returned from this action
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

        //return the viewModel and direct the user to the CustomerForm (rather than New that would have been the default as per the action name
        return View("CustomerForm", viewModel);
        }


        //Create() will be called when Customer/New View is closed (which is when the Save button is clicked)
        //Create() can only be only be called using HttpPost and not HttpGet
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
            }

            //establish if the customer is a new customer and therefore needs to be created (in which case their Id will be 0)
            if (customer.Id == 0)
            {
                //add the data passed to the action to the Customers DbContext
                _context.Customers.Add(customer);
            }
            else
            {
                //create an instance of the customer that already exists and update it's details with that of 'customer' that was passed to this action
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            //commit the changes to the database - i.e. add the new customer data to the database or update the existing customer in the database with customerInDb new values
            _context.SaveChanges();

            //now want to redirect the user to the list of customers
            return RedirectToAction("Index", "Customers");
        }


        public ViewResult Index()
        {
            //get data from Customers table
            //because the Customers table is linked to the MembershipType table, that linked data also needs to be loaded and therefore have to use the .Include() method
            //the .ToList() method ensures that the query is run at this point
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            // create and populate a viewModel of the CustomerFormViewModel type with the specific customer that is to be edited and a list of the possible membership types that can be used when editing the customer
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };


            return View("CustomerForm", viewModel);
        }
    }
}