using AspDotnetMvcToyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.Repositories
{
    public interface ISkillRepository
    {
        // Returns list of skills
        Task<List<Skill>> GetSkills();
    }
}
