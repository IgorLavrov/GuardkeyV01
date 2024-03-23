using GuardkeyV01.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardkeyV01.Services
{
    public interface INoteRepository
    {
        Task<bool> AddUserRecordAsync(Note record);
        Task<bool> UpdateUserRecordAsync(Note record);

        Task<bool> DeleteUserRecordAsync(int id);

        Task<Note> GetUserRecordAsync(int id);

        Task<IEnumerable<Note>> GetUserRecordsAsync();

        Task<IEnumerable<Note>> SortRecord(string name);

        Task<List<Note>> SortRecordByPicker(string name);



    }
}
