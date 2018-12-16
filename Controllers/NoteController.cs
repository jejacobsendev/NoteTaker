using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoteTaker.Models;
using NoteTaker.DAL;

namespace NoteTaker.Controllers
{
  public class NoteController : Controller
  {
    private readonly NoteRepository noteRepository = new NoteRepository();

    // GET: Cards
    public async Task<IActionResult> Index()
    {
      var notes = await noteRepository.GetAllAsync();

      return View(notes);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string Title, string NoteText)
    {
      var note = new Note()
      {
        Title = Title,
        NoteText = NoteText
      };

      await noteRepository.CreateAsync(note);

      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var note = await noteRepository.GetByIdAsync((int)id);
      if (note == null)
      {
        return NotFound();
      }

      return View(note);
    }

    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var note = await noteRepository.GetByIdAsync((int)id);
      if (note == null)
      {
        return NotFound();
      }

      return View(note);
    }

    // POST: Deck/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int NoteId, string Title, string NoteText)
    {
      var note = new Note()
      {
        NoteId = NoteId,
        Title = Title,
        NoteText = NoteText
      };

      await noteRepository.UpdateAsync(note);

      return RedirectToAction(nameof(Index));
    }

    // GET: Deck/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var note = await noteRepository.GetByIdAsync((int)id);

      if (note == null)
      {
        return NotFound();
      }

      return View(note);
    }

    // POST: Deck/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      await noteRepository.DeleteAsync(id);

      return RedirectToAction(nameof(Index));
    }
  }
}
