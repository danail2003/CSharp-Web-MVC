using SUS.HTTP;
using SUS.MvcFramework;
using BattleCards.Services;
using BattleCards.ViewModels.Cards;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly ICardsService service;

        public CardsController(ICardsService service)
        {
            this.service = service;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CardsInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.Name) || model.Name.Length < 5 || model.Name.Length > 15)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(model.Keyword))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(model.Image) || !model.Image.Contains("https://"))
            {
                return this.View();
            }

            if (model.Health < 0)
            {
                return this.View();
            }

            if (model.Attack < 0)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(model.Description) || model.Description.Length > 200)
            {
                return this.View();
            }

            string userId = this.GetUserId();

            this.service.Create(model, userId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.service.GetAll();

            return this.View(viewModel);
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            string userId = this.GetUserId();

            var viewModel = this.service.GetMyCards(userId);

            return this.View(viewModel);
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            string userId = this.GetUserId();

            if (this.service.IsCardInTheCollection(userId, cardId))
            {
                return this.Redirect("/Cards/All");
            }

            this.service.AddCardToUser(userId, cardId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            string userId = this.GetUserId();

            this.service.RemoveCardFromUser(userId, cardId);

            return this.Redirect("/Cards/Collection");
        }
    }
}
