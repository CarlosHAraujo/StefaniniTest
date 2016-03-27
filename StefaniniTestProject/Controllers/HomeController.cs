using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using StefaniniTestProject.Models;
using StefaniniTestProject.Repositories;

namespace StefaniniTestProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly CustomerRepository _customerRepository;
        private readonly LoginRepository _loginRepository;
        private readonly SellerRepository _sellerRepository;
        private readonly CityRepository _cityRepository;
        private readonly RegionRepository _regionRepository;
        private readonly ClassificationRepository _classificationRepository;

        public HomeController()
        {
            _customerRepository = new CustomerRepository();
            _sellerRepository = new SellerRepository();
            _loginRepository = new LoginRepository();
            _cityRepository = new CityRepository();
            _regionRepository = new RegionRepository();
            _classificationRepository = new ClassificationRepository();
        }

        [Route("Customer/List")]
        public ActionResult Index(SearchCustomerViewModel model)
        {
            ViewBag.Sellers = new SelectList(_sellerRepository.GetSellers(), "Id", "Name", model == null ? null : model.SellerId);
            ViewBag.Cities = new SelectList(_cityRepository.GetCities(), "Id", "Name", model == null ? null : model.CityId);
            ViewBag.Regions = new SelectList(_regionRepository.GetRegions(model.CityId), "Id", "Name", model == null ? null : model.RegionId);
            ViewBag.Classifications = new SelectList(_classificationRepository.GetClassifications(), "Id", "Name", model == null ? null : model.ClassificationId);
            ViewData["IsAdmin"] = _loginRepository.IsAdmin(this.User.Identity.Name);
            return View(new CustomerListViewModel(_customerRepository.GetCustomers(model, this.User.Identity.Name)) { Search = model });
        }

        public JsonResult GetRegions(string cityId)
        {
            return Json(new SelectList(_regionRepository.GetRegions(cityId), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
    }
}