using CarStore_ControllerAPI.Models;
using CarStore_ControllerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarStore_ControllerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    public CarController() { }

    // GET: All cars
    [HttpGet]
    public ActionResult<List<CarModel>> GetCars() => CarService.GetAll();

    // GET: By Id
    [HttpGet("{id}")]
    public ActionResult<CarModel> Get(int id)
    {
        var Car = CarService.Get(id);

        if (Car == null)
            return NotFound();

        return Car;
    }

    // POST: Create a new Car
    [HttpPost]
    public IActionResult Create(CarModel Car)
    {
        CarService.Add(Car);
        return CreatedAtAction(nameof(Get), new { id = Car.Id }, Car);
    }

    // PUT: Update an existing Car
    [HttpPut("{id}")]
    public IActionResult Update(int id, CarModel Car)
    {
        if (id != Car.Id)
            return BadRequest();

        var existingCar = CarService.Get(id);
        if (existingCar is null)
            return NotFound();

        CarService.Update(Car);

        return NoContent();
    }

    // DELETE: Delete an existing Car
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var Car = CarService.Get(id);

        if (Car is null)
            return NotFound();

        CarService.Delete(id);

        return NoContent();
    }
}