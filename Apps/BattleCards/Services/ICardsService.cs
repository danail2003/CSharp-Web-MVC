using System.Collections.Generic;
using BattleCards.ViewModels.Cards;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        void Create(CardsInputModel model, string userId);

        IEnumerable<CardsViewModel> GetAll();

        IEnumerable<CardsViewModel> GetMyCards(string userId);

        void AddCardToUser(string userId, int cardId);

        bool IsCardInTheCollection(string userId, int cardId);

        void RemoveCardFromUser(string userId, int cardId);
    }
}
