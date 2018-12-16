using System;
using System.Collections.Generic;

namespace NoteTaker.Models
{
  public partial class Note
  {
    public int NoteId { get; set; }
    public string Title { get; set; }
    public string NoteText { get; set; }
  }
}