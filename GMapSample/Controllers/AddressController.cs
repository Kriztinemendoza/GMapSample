using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GMapSample.DataModel;
using GMapSample.DataContract;

namespace GMapSample.Controllers
{
    public class AddressController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IAddressRepository _repository;

        //TODO: Refactor IUnitOfWork so the I____Repository can be removed from the constructor
        public AddressController(IUnitOfWork uow, IAddressRepository repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Note: 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Address address)
        {
            //TODO: add validation
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Add(address);
                    _uow.Commit();
                }
                catch (Exception ex)
                {
                    //Logger.Error(ex.Message); //Logging here or in the global.asax
                    return Json(false);
                }
              
                return Json(true); //return true or false for simplicity purpose
            }

            return Json(false);
        }
    }
}
