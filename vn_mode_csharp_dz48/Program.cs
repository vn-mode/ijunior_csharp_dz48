using System;
using System.Collections.Generic;

public class Fish
{
    private const int MaxAge = 10;

    private int _age;

    public string Name { get; private set; }
    public int Age
    {
        get { return _age; }
        private set { _age = value; }
    }

    public bool IsDead => _age > MaxAge;

    public Fish(string name)
    {
        Name = name;
        _age = 0;
    }

    public void IncrementAge()
    {
        _age++;
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

    private void AddFish()
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

    private void RemoveFish()
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

    public bool Interact()
    {
        Console.WriteLine("Введите команду (add, remove, show, quit):");
        var command = Console.ReadLine().Trim().ToLower();

        switch (command)
        {
            case AddCommand:
                AddFish();
                break;

            case RemoveCommand:
                RemoveFish();
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
