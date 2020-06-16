using AspDotnetMvcToyApp.Data;
using AspDotnetMvcToyApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.Repositories
{
    public class SkillRepository : ISkillRepository, IDisposable
    {

        private ToyAppContext _context;
        private bool _disposed = false;

        public SkillRepository(ToyAppContext context)
        {
            _context = context;
        }


        public async Task<List<Skill>> GetSkills()
        {
            return await _context.Skills.ToListAsync<Skill>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
