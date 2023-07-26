using System;
using System.Collections.Generic;

public class Fish
{
    public string Name { get; private set; }
    public int Age { get; private set; }

    public Fish(string name)
    {
        Name = name;
        Age = 0;
    }

    public bool IsDead => Age > 10;

    public void IncrementAge()
    {
        Age++;
    }
}

public class Aquarium
{
    private readonly int _capacity;
    private List<Fish> _fishes = new List<Fish>();

    public Aquarium(int capacity)
    {
        _capacity = capacity;
    }

    public bool Interact()
    {
        Console.WriteLine("Введите команду (add, remove, show, quit):");
        var command = Console.ReadLine().Trim().ToLower();

        switch (command)
        {
            case "add":
                AddFishPrompt();
                break;

            case "remove":
                RemoveFishPrompt();
                break;

            case "show":
                ShowFishes();
                break;

            case "quit":
                return false;

            default:
                Console.WriteLine("Неизвестная команда, попробуйте еще раз.");
                break;
        }

        return true;
    }

    public void Live()
    {
        foreach (var fish in _fishes)
        {
            fish.IncrementAge();
        }
    }

    public void ShowFishes()
    {
        foreach (var fish in _fishes)
        {
            Console.WriteLine($"Имя: {fish.Name}, Возраст: {fish.Age}");
        }
    }

    private void AddFishPrompt()
    {
        if (_fishes.Count >= _capacity)
        {
            Console.WriteLine("Аквариум полон, нельзя добавить еще одну рыбу.");
            return;
        }

        Console.Write("Введите имя рыбы: ");
        var name = Console.ReadLine();
        AddFish(new Fish(name));
    }

    private void AddFish(Fish fish)
    {
        _fishes.Add(fish);
    }

    private void RemoveFishPrompt()
    {
        Console.Write("Введите имя рыбы для удаления: ");
        var name = Console.ReadLine();
        RemoveFish(name);
    }

    private void RemoveFish(string name)
    {
        if (_fishes.RemoveAll(fish => fish.Name == name) > 0)
        {
            Console.WriteLine($"Рыба {name} была удалена из аквариума.");
        }
        else
        {
            Console.WriteLine($"Рыба с именем {name} не найдена в аквариуме.");
        }
    }

    public void RemoveDeadFishes()
    {
        for (int i = _fishes.Count - 1; i >= 0; i--)
        {
            if (_fishes[i].IsDead)
            {
                Console.WriteLine($"Рыба {_fishes[i].Name} умерла от старости.");
                _fishes.RemoveAt(i);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var aquarium = new Aquarium(3);

        while (aquarium.Interact())
        {
            aquarium.Live();
            aquarium.RemoveDeadFishes();
        }
    }
}
