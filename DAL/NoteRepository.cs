using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NoteTaker.Models;
using System.Configuration;

namespace NoteTaker.DAL
{
  public class NoteRepository : IRepository<Note>
  {
    private string defaultConnection = ConfigurationManager.AppSettings["DefaultConnection"];
    public async Task<List<Note>> GetAllAsync()
    {
      var notes = new List<Note>();

      using (var conn = new MySqlConnection(defaultConnection))
      {
        string sql = $"SELECT * FROM notes";

        var command = new MySqlCommand(sql, conn);
        conn.Open();

        using (var reader = await command.ExecuteReaderAsync())
        {
          while (reader.Read())
          {
            var id = reader.GetInt32(0);
            var title = reader.GetString(1);
            var noteText = reader.GetString(2);

            var note = new Note()
            {
              NoteId = id,
              Title = title,
              NoteText = noteText
            };

            notes.Add(note);
          }
        }
      }

      return notes;
    }

    public async Task<Note> GetByIdAsync(int id)
    {
      var note = new Note();
      using (var conn = new MySqlConnection(defaultConnection))
      {
        string sql = $"SELECT * FROM notes where note_id = {id}";

        var command = new MySqlCommand(sql, conn);
        conn.Open();

        using (var reader = await command.ExecuteReaderAsync())
        {
          while (reader.Read())
          {
            var noteId = reader.GetInt32(0);
            var title = reader.GetString(1);
            var noteText = reader.GetString(2);

            note.NoteId = noteId;
            note.Title = title;
            note.NoteText = noteText;
          }
        }
      }

      return note;
    }

    public async Task CreateAsync(Note note)
    {
      var title = note.Title;
      var noteText = note.NoteText;
      using (var conn = new MySqlConnection(defaultConnection))
      {
        conn.Open();

        using (var command = conn.CreateCommand())
        {
          command.CommandText = "insert into notes (title, note_text) values (@title, @noteText)";
          command.Parameters.AddWithValue("@title", title);
          command.Parameters.AddWithValue("@noteText", noteText);
          await command.ExecuteNonQueryAsync();
        }
      }
    }

    public async Task UpdateAsync(Note note)
    {
      var id = note.NoteId;
      var title = note.Title;
      var noteText = note.NoteText;
      using (var conn = new MySqlConnection(defaultConnection))
      {
        conn.Open();

        using (var command = conn.CreateCommand())
        {
          command.CommandText = "update notes set title=@title, note_text=@noteText where note_id=@id";
          command.Parameters.AddWithValue("@title", title);
          command.Parameters.AddWithValue("@noteText", noteText);
          command.Parameters.AddWithValue("@id", id);
          await command.ExecuteNonQueryAsync();
        }
      }
    }

    public async Task DeleteAsync(int id)
    {
      using (var conn = new MySqlConnection(defaultConnection))
      {
        conn.Open();

        using (var command = conn.CreateCommand())
        {
          command.CommandText = $"delete from notes where note_id=@id";
          command.Parameters.AddWithValue("@id", id);
          await command.ExecuteNonQueryAsync();
        }
      }
    }
  }
}