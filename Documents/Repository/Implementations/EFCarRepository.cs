using Documents.Models;
using Documents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Documents.Repository.Implementations
{
    public class EFCarRepository : ICarRepository
    {
        DocumentsEntities dc = new DocumentsEntities();
        public IEnumerable<Models.cars> GetCars()
        {
            return dc.cars;
        }

        public Models.cars GetCarById(int id)
        {
            return dc.cars.FirstOrDefault(x => x.CarId == id);
        }
    }
}