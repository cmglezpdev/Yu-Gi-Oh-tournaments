using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Deck;

namespace backend.Application.Repositories{

    public interface IDeckRepository:IRepository{
      Task<Deck> GetDeckByIdAsync(Guid Id);
      Task<Deck> CreateDeckAsync(DeckInputDto deck);
      Task<Deck> UpdateDeckAsync(Guid Id, DeckInputDto deck);
      Task<Deck> DeleteDeckByIdAsync(Guid Id);

    }
}