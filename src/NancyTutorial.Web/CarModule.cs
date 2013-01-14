using Nancy;
using Nancy.ModelBinding;

namespace NancyTutorial
{
    public class CarModule : NancyModule
    {
        private readonly ICarRepository _repository;

        public CarModule(ICarRepository repository)
        {
            _repository = repository;
            Get["/status"] = _ => "Hello World";

            Get["/car/{id}"] = parameters =>
                                   {
                                       int id = parameters.id;

                                       var carModel = _repository.GetById(id);

                                       return Negotiate
                                           .WithStatusCode(HttpStatusCode.OK)
                                           .WithModel(carModel);
                                   };

            Get["/{make}/{model}"] = parameters =>
                                         {
                                             var carQuery = this.Bind<BrowseCarQuery>();

                                             var listOfCars = _repository.GetListOf(carQuery);

                                             return Negotiate
                                                 .WithStatusCode(HttpStatusCode.OK)
                                                 .WithModel(listOfCars);
                                         };
        }
    }
}