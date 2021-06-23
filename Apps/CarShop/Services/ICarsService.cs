namespace CarShop.Services
{
    using CarShop.ViewModels.Cars;
    using System.Collections.Generic;

    public interface ICarsService
    {
        void Create(AddCarInputModel model, string userId);

        IEnumerable<AllCarsViewModel> GetMyCars(string userId);

        IEnumerable<AllCarsViewModel> GetAllCars();
    }
}
