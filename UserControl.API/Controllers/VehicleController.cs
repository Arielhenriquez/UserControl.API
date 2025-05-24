using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserControl.Core.Enums;
using UserControl.Model.Entities;

namespace UserControl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleController : ControllerBase
{
    private static List<Vehicle> _vehicles = new()
    {
        new Vehicle
        {
            Id = 1,
            Brand = "Toyota",
            Model = "Corolla",
            Plate = "ABC123",
            IsAvailable = true,
            Fuel = FuelType.Gasoline,
            ManufactureDate = new DateTime(2018, 5, 10),
            Features = new List<string> { "Bluetooth", "Aire acondicionado" },
            Price = 15000,
            MileageKm = 45000
        },
    };

    [HttpGet]
    [SwaggerOperation(Summary = "Obtiene todos los vehículos registrados.")]
    [ProducesResponseType(typeof(List<Vehicle>), 200)]
    [ProducesResponseType(500)]
    public IActionResult Get()
    {
        return Ok(_vehicles);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtiene un vehículo específico por ID.")]
    [ProducesResponseType(typeof(Vehicle), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult Get(int id)
    {
        var vehicle = _vehicles.FirstOrDefault(v => v.Id == id);
        if (vehicle is null)
            return NotFound("Vehículo no encontrado.");

        return Ok(vehicle);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Crea un nuevo vehículo. La placa debe ser única.")]
    [ProducesResponseType(typeof(Vehicle), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public IActionResult Post([FromBody] Vehicle newVehicle)
    {
        if (newVehicle is null)
            return BadRequest("Datos inválidos.");

        bool placaExiste = _vehicles.Any(v => v.Plate.Equals(newVehicle.Plate, StringComparison.OrdinalIgnoreCase));
        if (placaExiste)
            return BadRequest("Ya existe un vehículo con esa placa.");

        try
        {
            newVehicle.Id = _vehicles.Max(v => v.Id) + 1;
            _vehicles.Add(newVehicle);
            return CreatedAtAction(nameof(Get), new { id = newVehicle.Id }, newVehicle);
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Actualiza completamente un vehículo por ID.")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult Put(int id, [FromBody] Vehicle updated)
    {
        if (updated is null)
            return BadRequest("Datos inválidos.");

        var index = _vehicles.FindIndex(v => v.Id == id);
        if (index == -1)
            return NotFound("Vehículo no encontrado.");

        // Validar placa única (excepto si es el mismo vehículo)
        bool placaExiste = _vehicles.Any(v =>
            v.Id != id &&
            v.Plate.Equals(updated.Plate, StringComparison.OrdinalIgnoreCase));

        if (placaExiste)
            return BadRequest("Ya existe otro vehículo con esa placa.");

        try
        {
            updated.Id = id;
            _vehicles[index] = updated;
            return Ok("Vehículo actualizado correctamente.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Error al actualizar el vehículo.");
        }
    }

    [HttpPatch("{id}")]
    [SwaggerOperation(Summary = "Actualiza solo la disponibilidad del vehículo.")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult Patch(int id, [FromBody] bool isAvailable)
    {
        var vehicle = _vehicles.FirstOrDefault(v => v.Id == id);
        if (vehicle is null)
            return NotFound("Vehículo no encontrado.");

        try
        {
            vehicle.IsAvailable = isAvailable;
            return Ok("Disponibilidad actualizada correctamente.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Error al actualizar la disponibilidad.");
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Elimina un vehículo por ID.")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult Delete(int id)
    {
        var vehicle = _vehicles.FirstOrDefault(v => v.Id == id);
        if (vehicle is null)
            return NotFound("Vehículo no encontrado.");

        try
        {
            _vehicles.Remove(vehicle);
            return Ok("Vehículo eliminado correctamente.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Error al eliminar el vehículo.");
        }
    }

}
