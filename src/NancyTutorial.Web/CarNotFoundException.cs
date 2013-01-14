using System;

namespace NancyTutorial
{
    public class CarNotFoundException : Exception
    {
        public CarNotFoundException(string message) : base(message){}
    }
}