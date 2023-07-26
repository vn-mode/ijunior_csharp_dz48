using System;
using System.Collections.Generic;

class Program
{
    private static readonly string AddCommand = "add";
    private static readonly string RemoveCommand = "remove";
    private static readonly string ShowCommand = "show";
    private static readonly string QuitCommand = "quit";

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
    private const string AddCommand = "add";
    private const string RemoveCommand = "remove";
    private const string ShowCommand = "show";
    private const string QuitCommand = "quit";

    private readonly int _capacity;
    private List<Fish> _fishes;

    public Aquarium(int capacity)
    {
        _capacity = capacity;
        _fishes = new List<Fish>();
    }

    public void Live()
    {
        _fishes.ForEach(fish => fish.IncrementAge());
    }

    public void ShowFishes()
    {
        _fishes.ForEach(fish => Console.WriteLine($"Имя: {fish.Name}, Возраст: {fish.Age}"));
    }

    public void RemoveDeadFishes()
    {
        _fishes.RemoveAll(fish => fish.IsDead);
    }

    public bool Interact()
    {
        Console.WriteLine("Введите команду (add, remove, show, quit):");
        var command = Console.ReadLine().Trim().ToLower();

        switch (command)
        {
            case AddCommand:
                AddFishPrompt();
                break;

            case RemoveCommand:
                RemoveFishPrompt();
                break;

            case ShowCommand:
                ShowFishes();
                break;

            case QuitCommand:
                return false;

            default:
                Console.WriteLine("Неизвестная команда, попробуйте еще раз.");
                break;
        }
        return true;
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
        _fishes.Add(new Fish(name));
    }

    private void RemoveFishPrompt()
    {
        Console.Write("Введите имя рыбы для удаления: ");
        var name = Console.ReadLine();
        var removedCount = _fishes.RemoveAll(fish => fish.Name == name);

        if (removedCount > 0)
        {
            Console.WriteLine($"Рыба {name} была удалена из аквариума.");
        }
        else
        {
            Console.WriteLine($"Рыба с именем {name} не найдена в аквариуме.");
        }
    }
}
