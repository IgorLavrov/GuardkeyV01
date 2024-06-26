﻿using GuardkeyV01.Models;
using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace GuardkeyV01.Services
{
    public class NoteService :INoteRepository
    {
        private SQLiteAsyncConnection _database;

        public NoteService(SQLiteAsyncConnection database)
        {
            _database = database;
        }
    
        public async Task<bool> AddUserRecordAsync(Note record)
        {
            if (record.NoteId > 0)
            {
                await _database.UpdateAsync(record);
            }
            else
            {
                await _database.InsertAsync(record); 
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteUserRecordAsync(int id)
        {
            await _database.DeleteAsync<Note>(id);
            return await Task.FromResult(true);
        }
        public async Task<Note> GetUserRecordAsync(int id)
        {
            return await _database.Table<Note>().Where(p => p.NoteId == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Note>> SortRecord(string name)
        {
            return await _database.Table<Note>().Where(value => value.ResourceName.ToLower().Contains(name.ToLower()))
    .ToListAsync();
        }

        public async Task<List<Note>> SortRecordByPicker(string name)
        {
            var allNotes = await _database.Table<Note>().ToListAsync();

            var filteredNotes = allNotes.Where(note => note.Categories == name);

            return filteredNotes.ToList();
        }

        public async Task<IEnumerable<Note>> GetUserRecordsAsync()
        {
            return await Task.FromResult(await _database.Table<Note>().ToListAsync());

        }
        public async Task<bool> UpdateUserRecordAsync(Note record)
        {

            if (record.NoteId > 0)
            {
                await _database.UpdateAsync(record);
            }
            else
            {
                await _database.InsertAsync(record);

            }
            return await Task.FromResult(true);
        }

      
    }

}

