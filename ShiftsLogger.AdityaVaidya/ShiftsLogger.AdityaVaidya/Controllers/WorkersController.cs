﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.AdityaVaidya.Data;
using ShiftsLogger.AdityaVaidya.Models;

namespace ShiftsLogger.AdityaVaidya.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkersController : ControllerBase
{
    private readonly ShiftContext _context;

    public WorkersController(ShiftContext context)
    {
        _context = context;
    }

    // GET: api/Workers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Worker>>> GetWorkers()
    {
        return await _context.Workers.ToListAsync();
    }

    // GET: api/Workers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Worker>> GetWorker(int id)
    {
        var worker = await _context.Workers.FindAsync(id);

        if (worker == null)
        {
            return NotFound();
        }

        return worker;
    }

    // PUT: api/Workers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorker(int id, Worker worker)
    {
        if (id != worker.WorkerId)
        {
            return BadRequest();
        }

        _context.Entry(worker).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WorkerExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Workers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Worker>> PostWorker(Worker worker)
    {
        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetWorker", new { id = worker.WorkerId }, worker);
    }

    // DELETE: api/Workers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorker(int id)
    {
        var worker = await _context.Workers.FindAsync(id);
        if (worker == null)
        {
            return NotFound();
        }

        _context.Workers.Remove(worker);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WorkerExists(int id)
    {
        return _context.Workers.Any(e => e.WorkerId == id);
    }
}