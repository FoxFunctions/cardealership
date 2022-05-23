using System;
using CarDealership.models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.Controllers
{

	[ApiController]
	[Route("controller")]
	public class CarsController
	{
		CarsContext context = new CarsContext();
		public List<Car> cars = new List<Car>();

		[HttpGet("ShowAllCars")]
		public List<Car> ShowAllCars()
		{
			return context.Cars.ToList();
		}

		[HttpGet("SearchCarByIndex/{id}")]
		public Car SearchCarByIndex(int id)
		{
			try
			{
				Car output = context.Cars.Find(id);
				return output;
			}
			catch (Exception e)
			{
				Car c = new Car();
				c.Make = e.Message;
				return c;
			}
		}

		[HttpGet("SearchByMake/{make}")]
		public List<Car> SearchByMake(string make)
		{
			List<Car> result = context.Cars.Where(x => x.Make == make).ToList();
			return result;
		}

		[HttpGet("SearchByModel/{model}")]
		public List<Car> SearchByModel(string model)
		{
			List<Car> result = context.Cars.Where(x => x.Model == model).ToList();
			return result;
		}

		[HttpGet("SearchByYear/{year}")]
		public List<Car> SearchByYear(int year)
		{
			List<Car> result = context.Cars.Where(x => x.Year == year).ToList();
			return result;
		}

		[HttpGet("SearchByColor/{color}")]
		public List<Car> SearchByColor(string color)
		{
			List<Car> result = context.Cars.Where(x => x.Color == color).ToList();
			return result;
		}

		[HttpPost("Create")]
		public void CreateCar(Car input)
		{
			context.Cars.Add(input);
			context.SaveChanges();
		}
		
		[HttpDelete("Delete/{id}")]
		public string DeleteCar(int id)
		{
			int initialCount = context.Cars.Count();
			try
			{
				context.Cars.Remove(SearchCarByIndex(id));
			}
			catch (Exception e)
			{
				string errorOutput = e.Message;
				errorOutput += "\n No Changes made to the database";
				return errorOutput;
			}
			context.SaveChanges();
			int finalCount = context.Cars.Count();

			return $"Began with {initialCount} cars and ended with {finalCount} cars.";
		}

		[HttpPut("Update/{id}")]
		public string UpdateCar(int id, Car updatedCar)
        {
			Car car = SearchCarByIndex(id);
			car.Make = updatedCar.Make;
			car.Model = updatedCar.Model;
			car.Year = updatedCar.Year;
			car.Color = updatedCar.Color;
			context.Cars.Update(car);
			context.SaveChanges();
			return $"{updatedCar.Year} {updatedCar.Make} {updatedCar.Model} has been updated";
        }
	}
}

