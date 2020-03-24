using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryCatalog.Controllers
{
  public class TitlesController : Controller
  {
    private readonly LibraryCatalogContext _db;

    public TitlesController(LibraryCatalogContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Titles.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Title title, int AuthorId)
    {
      _db.Titles.Add(title);
      if (AuthorId != 0)
      {
        _db.Book.Add(new Book() { AuthorId = AuthorId, TitleId = title.TitleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTitle = _db.Titles
          .Include(title => title.Authors)
          .ThenInclude(join => join.Author)
          .FirstOrDefault(title => title.TitleId == id);
      return View(thisTitle);
    }

    public ActionResult Edit(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(titles => titles.TitleId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View(thisTitle);
    }

    [HttpPost]
    public ActionResult Edit(Title title, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.Book.Add(new Book() { AuthorId = AuthorId, TitleId = title.TitleId });
      }
      _db.Entry(title).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddAuthor(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(titles => titles.TitleId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View(thisTitle);
    }

    [HttpPost]
    public ActionResult AddAuthor(Title title, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.Book.Add(new Book() { AuthorId = AuthorId, TitleId = title.TitleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(titles => titles.TitleId == id);
      return View(thisTitle);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(titles => titles.TitleId == id);
      _db.Titles.Remove(thisTitle);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAuthor(int joinId)
    {
      var joinEntry = _db.Book.FirstOrDefault(entry => entry.BookId == joinId);
      _db.Book.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}