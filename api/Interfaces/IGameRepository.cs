using api.Models;

namespace api.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<Game> GetGameByIdAsync(Guid id);
        Task<bool> CreateGameAsync(Game game);
        Task<bool> UpdateGameAsync(Game game);
        Task<bool> DeleteGameAsync(Guid id);
    }
}