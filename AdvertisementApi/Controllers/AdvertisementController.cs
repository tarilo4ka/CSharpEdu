// using Microsoft.AspNetCore.Mvc;
// using AdvertisementApi.Models;

// namespace AdvertisementApi.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class AdvertisementController : ControllerBase
// {
//     private readonly DataContext _context;

//     public AdvertisementController(DataContext context)
//     {
//         _context = context;
//     }

//     [HttpGet]
//     public async Task<ActionResult<List<Advertisement>>> Get()
//     {
//         if (_context.Advertisements == null)
//         {
//             return BadRequest("Advertisements not found");
//         }
//         return Ok(await _context.Advertisements.ToListAsync());
//     }

//     [HttpGet("{id}")]
//     public async Task<ActionResult<Advertisement>> Get(int id)
//     {
//         if (_context.Advertisements == null)
//         {
//             return BadRequest("Advertisements not found");
//         }


//         var advertisement = await _context.Advertisements.FindAsync(id);

//         if (advertisement == null)
//         {
//             return NotFound("Advertisement not found");
//         }
//         return Ok(advertisement);
//     }

//     [HttpGet("searchBy/{searchBy}")]
//     public async Task<ActionResult<List<Advertisement>>> Get(string searchBy)
//     {
//         if (_context.Advertisements == null)
//         {
//             return BadRequest("Advertisements not found");
//         }
//         var advertisements = await _context.Advertisements.ToListAsync();

//         if (searchBy != null)
//         {
//             advertisements = advertisements.Where(a => 
//                    a.Title.Contains(searchBy) 
//                 || a.TransactionNumber.Contains(searchBy)
//                 || a.ImageUrl.Contains(searchBy)
//                 || a.WebsiteUrl.Contains(searchBy)
//                 || a.Price.ToString().Contains(searchBy)
//                 || a.StartDate.ToString().Contains(searchBy)
//                 || a.EndDate.ToString().Contains(searchBy)
//                 || a.Id.ToString().Contains(searchBy)
//                 ).ToList();
//         }
//         return Ok(advertisements);
//     }

//     [HttpGet("sortBy/{sortBy}|order/{order}")]
//     public async Task<ActionResult<List<Advertisement>>> Get(string sortBy, string order)
//     {
//         if (_context.Advertisements == null)
//         {
//             return BadRequest("Advertisements not found");
//         }
//         var advertisements = await _context.Advertisements.ToListAsync();

//          if (order == "asc")
//         {
//             advertisements = advertisements.OrderBy(a => a?.GetType()?.GetProperty(sortBy)?.GetValue(a, null)).ToList();
//         }
//         else if (order == "desc")
//         {
//             advertisements = advertisements.OrderByDescending(a => a?.GetType()?.GetProperty(sortBy)?.GetValue(a, null)).ToList();
//         }
//         else
//         {
//             return BadRequest("Invalid order");
//         }

//         return Ok(advertisements);
//     }
    
//     [HttpGet("sortBy/{sortBy}/order/{order}/searchBy/{searchBy}")]
//     public async Task<ActionResult<List<Advertisement>>> Get(string sortBy, string order, string searchBy)
//     {
//         if (_context.Advertisements == null)
//         {
//             return BadRequest("Advertisements not found");
//         }
//         var advertisements = await _context.Advertisements.ToListAsync();

//         if (searchBy != null)
//         {
//             advertisements = advertisements.Where(a => 
//                    a.Title.Contains(searchBy) 
//                 || a.TransactionNumber.Contains(searchBy)
//                 || a.ImageUrl.Contains(searchBy)
//                 || a.WebsiteUrl.Contains(searchBy)
//                 || a.Price.ToString().Contains(searchBy)
//                 || a.StartDate.ToString().Contains(searchBy)
//                 || a.EndDate.ToString().Contains(searchBy)
//                 || a.Id.ToString().Contains(searchBy)
//                 ).ToList();
//         }

//         if (order == "asc")
//         {
//             advertisements = advertisements.OrderBy(a => a?.GetType()?.GetProperty(sortBy)?.GetValue(a, null)).ToList();
//         }
//         else if (order == "desc")
//         {
//             advertisements = advertisements.OrderByDescending(a => a?.GetType()?.GetProperty(sortBy)?.GetValue(a, null)).ToList();
//         }
//         else
//         {
//             return BadRequest("Invalid order");
//         }

//         return Ok(advertisements);
//     }
       
    


//     [HttpPost]
//     public async Task<ActionResult<Advertisement>> Post(Advertisement advertisement)
//     {
//         _context.Advertisements?.Add(advertisement);
//         await _context.SaveChangesAsync();

//         return CreatedAtAction(nameof(Get), new { id = advertisement.Id }, advertisement);
//     }

//     [HttpPut]
//     public async Task<ActionResult<Advertisement>> Put(Advertisement request)
//     {
//         if (_context.Advertisements == null)
//         {
//             return BadRequest("Advertisements not found");
//         }

//         var dbAdvertisement = await _context.Advertisements.FindAsync(request.Id);
//         if (dbAdvertisement == null)
//         {
//             return BadRequest("Advertisement not found");
//         }

//         dbAdvertisement.Title = request.Title;
//         dbAdvertisement.TransactionNumber = request.TransactionNumber;
//         dbAdvertisement.Price = request.Price;
//         dbAdvertisement.StartDate = request.StartDate;
//         dbAdvertisement.EndDate = request.EndDate;
//         dbAdvertisement.ImageUrl = request.ImageUrl;
//         dbAdvertisement.WebsiteUrl = request.WebsiteUrl;

//         await _context.SaveChangesAsync();

//         return Ok(await _context.Advertisements.ToListAsync());
//     }

//     [HttpDelete("{id}")]
//     public async Task<ActionResult<Advertisement>> Delete(int id)
//     {
//         if (_context.Advertisements == null)
//         {
//             return BadRequest("Advertisements not found");
//         }

//         var advertisement = await _context.Advertisements.FindAsync(id);
//         if (advertisement == null)
//         {
//             return BadRequest("Advertisement not found");
//         }

//         _context.Advertisements.Remove(advertisement);
//         await _context.SaveChangesAsync();

//         return Ok(await _context.Advertisements.ToListAsync());
//     }
// }
