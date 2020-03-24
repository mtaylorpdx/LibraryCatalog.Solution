using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryCatalog.Controllers
{
  public class PatronsController : Controller
  {
    private readonly LibraryCatalogContext _db;

    public PatronsController(LibraryCatalogContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Patrons.ToList());
    }

        public ActionResult Create()
    {
      ViewBag.PatronId = new SelectList(_db.Patrons, "PatronId", "PatronName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patron patron, int TitleId)
    {
      _db.Patrons.Add(patron);
      if (TitleId != 0)
      {
        _db.Checkout.Add(new Checkout() { PatronId = patron.PatronId, TitleId = TitleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
