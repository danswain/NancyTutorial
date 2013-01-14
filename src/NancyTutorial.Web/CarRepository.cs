using System.Collections.Generic;

namespace NancyTutorial
{
    public class CarRepository : ICarRepository
    {
        public Car GetById(int id)
        {
            if (id == 321)
                throw new CarNotFoundException("Car with Id:" + id + " Not Found");
            return new Car
                       {
                           Id = id,
                           Make = "Tesla",
                           Model = "Model S"
                       };
        }

        public IList<Car> GetListOf(BrowseCarQuery carQuery)
        {
            return new List<Car>
                       {
                           new Car
                               {
                                   Id = 1,
                                   Make = carQuery.Make,
                                   Model = carQuery.Model
                               },
                           new Car
                               {
                                   Id = 2,
                                   Make = carQuery.Make,
                                   Model = carQuery.Model
                               },
                           new Car
                               {
                                   Id = 3,
                                   Make = carQuery.Make,
                                   Model = carQuery.Model
                               }
                       };
        }
    }
}