using System.Collections.Generic;

namespace NancyTutorial
{
    public interface ICarRepository
    {
        Car GetById(int id);
        IList<Car> GetListOf(BrowseCarQuery carQuery);
    }
}