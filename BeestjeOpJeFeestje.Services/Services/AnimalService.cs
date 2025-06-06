﻿using BeestjeOpJeFeestje.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Services.Services;

public class AnimalService
{
    private readonly BeestjeOpJeFeestjeContext _context;
    public AnimalService(BeestjeOpJeFeestjeContext context)
    {
        _context = context; 
    }

    public List<Animal> GetAnimals()
    {
        var animals = _context.Animals.ToList();

        return animals;
    }

    public Animal GetAnimal(int id)
    {
        Animal animal = _context.Animals.FirstOrDefault(a => a.Id == id);

        return animal;
    }

    public List<Booking>? GetAnimalBookings(Animal animal)
    {
        List<Booking>? bookings = _context.Bookings.Where(b => b.Animals.Contains(animal)).ToList();

        return bookings;
    }

    public Array getAnimalTypes()
    {
        return Enum.GetValues(typeof(AnimalTypes));
    }

    public void CreateAnimal(Animal animal)
    {
        _context.Animals.Add(animal);
        _context.SaveChanges();
    }

    public void UpdateAnimal(Animal animal)
    {
        _context.Animals.Update(animal);
        _context.SaveChanges();
    }   

    public void DeleteAnimal(Animal animal)
    {
        _context.Animals.Remove(animal);
        _context.SaveChanges();
    }
}

