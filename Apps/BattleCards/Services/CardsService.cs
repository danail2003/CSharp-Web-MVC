using System.Collections.Generic;
using System.Linq;
using BattleCards.Data;
using BattleCards.Models;
using BattleCards.ViewModels.Cards;

namespace BattleCards.Services
{
    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;

        public CardsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddCardToUser(string userId, int cardId)
        {
            this.db.UserCards.Add(new UserCard
            {
                CardId = cardId,
                UserId = userId,
            });

            this.db.SaveChanges();
        }

        public void Create(CardsInputModel model, string userId)
        {
            Card card = new Card
            {
                Name = model.Name,
                Keyword = model.Keyword,
                ImageUrl = model.Image,
                Attack = model.Attack,
                Health = model.Health,
                Description = model.Description,
            };

            this.db.Cards.Add(card);

            this.db.UserCards.Add(new UserCard
            {
                UserId = userId,
                Card = card
            });

            this.db.SaveChanges();
        }

        public IEnumerable<CardsViewModel> GetAll()
        {
            return this.db.Cards.Select(x => new CardsViewModel
            {
                Name = x.Name,
                Keyword = x.Keyword,
                Description = x.Description,
                Attack = x.Attack,
                Health = x.Health,
                Image = x.ImageUrl,
                CardId = x.Id,
            }).ToList();
        }

        public IEnumerable<CardsViewModel> GetMyCards(string userId)
        {
            return this.db.UserCards.Where(x => x.UserId == userId).Select(x => new CardsViewModel
            {
                Name = x.Card.Name,
                Keyword = x.Card.Keyword,
                Image = x.Card.ImageUrl,
                Attack = x.Card.Attack,
                Health = x.Card.Health,
                Description = x.Card.Description,
                CardId = x.CardId
            }).ToList();
        }

        public bool IsCardInTheCollection(string userId, int cardId)
        {
            return this.db.UserCards.Any(x => x.CardId == cardId && x.UserId == userId);
        }

        public void RemoveCardFromUser(string userId, int cardId)
        {
            UserCard userCard = this.db.UserCards.FirstOrDefault(x => x.CardId == cardId && x.UserId == userId);

            this.db.UserCards.Remove(userCard);

            this.db.SaveChanges();
        }
    }
}
