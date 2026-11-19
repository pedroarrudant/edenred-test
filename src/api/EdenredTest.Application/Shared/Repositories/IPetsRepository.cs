using EdenredTest.Application.Features.GetPetsByName.Models;
using EdenredTest.Application.Shared.Models;

namespace EdenredTest.Application.Shared.Repositories
{
    public interface IPetsRepository
    {
        Task<Pet?> GetPetByNameAsync(string PetName, CancellationToken cancellationToken);

        Task<bool> InsertPetAsync(Pet pet, CancellationToken cancellationToken);
    }
}