using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var aquarium = new Aquarium(3);

        while (true)
        {
            aquarium.Interact();
            aquarium.Live();
            aquarium.RemoveDeadFishes();
        }
    }
}

public class Fish
{
    private const int _MaxAge = 10;
    private int _age;

    public Fish(string name)
    {
        Name = name;
        _age = 0;
    }

    public string Name { get; private set; }
    public int Age => _age;
    public bool IsDead => _age > _MaxAge;

    public void IncrementAge()
    {
        _age++;
    }
}

public class Aquarium
{
    private readonly int capacity;
    private List<Fish> fishes;

    public Aquarium(int capacity)
    {
        this.capacity = capacity;
        fishes = new List<Fish>();
    }

    public bool HasSpace()
    {
        return fishes.Count < capacity;
    }

    public void AddFish(Fish fish)
    {
        fishes.Add(fish);
    }

    public bool RemoveFish(string name)
    {
        var fish = fishes.FirstOrDefault(f => f.Name == name);

        if (fish != null)
        {
            fishes.Remove(fish);
            return true;
        }
        return false;
    }

    public void ShowFishes()
    {
        foreach (var fish in fishes)
        {
            Console.WriteLine($"Имя: {fish.Name}, Возраст: {fish.Age}");
        }
    }

    public void Interact()
    {
        const string AddCommand = "add";
        const string RemoveCommand = "remove";
        const string ShowCommand = "show";
        const string QuitCommand = "quit";

        Console.WriteLine("Введите команду (add, remove, show, quit):");
        string input = Console.ReadLine().Trim().ToLower();

        switch (input)
        {
            case AddCommand:
                AddFishCommand();
                break;

            case RemoveCommand:
                RemoveFishCommand();
                break;

            case ShowCommand:
                ShowFishCommand();
                break;

            case QuitCommand:
                Quit();
                break;

            default:
                UnknownCommand();
                break;
        }
    }

    private void AddFishCommand()
    {
        if (HasSpace())
        {
            Console.Write("Введите имя рыбы: ");
            var name = Console.ReadLine();
            AddFish(new Fish(name));
        }
        else
        {
            Console.WriteLine("Аквариум полон, нельзя добавить еще одну рыбу.");
        }
    }

    private void RemoveFishCommand()
    {
        Console.Write("Введите имя рыбы для удаления: ");
        var nameToRemove = Console.ReadLine();

        if (RemoveFish(nameToRemove))
        {
            Console.WriteLine($"Рыба {nameToRemove} была удалена из аквариума.");
        }
        else
        {
            Console.WriteLine($"Рыба с именем {nameToRemove} не найдена в аквариуме.");
        }
    }

    private void ShowFishCommand()
    {
        ShowFishes();
    }

    private void Quit()
    {
        Environment.Exit(0);
    }

    private void UnknownCommand()
    {
        Console.WriteLine("Неизвестная команда, попробуйте еще раз.");
    }

    public void Live()
    {
        foreach (var fish in fishes)
        {
            fish.IncrementAge();
        }
    }

    public void RemoveDeadFishes()
    {
        var deadFishes = fishes.Where(fish => fish.IsDead).ToList();

        foreach (var deadFish in deadFishes)
        {
            fishes.Remove(deadFish);
        }
    }
}
