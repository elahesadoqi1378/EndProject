using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Contracts.Repositroy
{
    public interface IExpertOfferRepository
    {
        Task<List<ExpertOffer>> GetAllAsync();
        Task<ExpertOffer> GetByIdAsync(int id);
        Task CreateAsync(ExpertOffer expertOffer);
        Task UpdateAsync(ExpertOffer expertOffer);
        Task DeleteAsync(int id);
    }
}
