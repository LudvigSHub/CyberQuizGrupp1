using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.DAL.Repositories.Interfaces
{
    public interface IUserResultRepository
    {
        Task<IEnumerable<object>> GetByUserIdAsync(string userId);
    }
}
