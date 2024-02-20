using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        Aquarium aquarium = new Aquarium(3);
        aquarium.Run();
    }
}

public class Fish
{
    private int _maxAge = 10;
    private int _age;

    public Fish(string name)
    {
        Name = name;
        _age = 0;
    }

    public string Name { get; private set; }
    public int Age => _age;
    public bool IsDead => (_age > _maxAge);

    public void IncrementAge()
    {
        _age++;
    }
}

public class Aquarium
{
    private readonly int _capacity;
    private List<Fish> _fishes;
    private bool isRunning = true;

    public Aquarium(int capacity)
    {
        _capacity = capacity;
        _fishes = new List<Fish>();
    }

    public bool HasSpace => _fishes.Count < _capacity;

    public void Run()
    {
        while (isRunning)
        {
            Interact();
            Live();
            RemoveDeadFishes();
        }
    }

    public void Interact()
    {
        const string AddCommand = "add";
        const string RemoveCommand = "remove";
        const string ShowCommand = "show";
        const string QuitCommand = "quit";

        Console.WriteLine($"Введите команду ({AddCommand}, {RemoveCommand}, {ShowCommand}, {QuitCommand}):");

        string input = Console.ReadLine().Trim().ToLower();

        switch (input)
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
                isRunning = false;
                break;

            default:
                Console.WriteLine("Неизвестная команда, попробуйте еще раз.");
                break;
        }
    }

    public void Live()
    {
        foreach (Fish fish in _fishes)
        {
            fish.IncrementAge();
        }
    }

    public void RemoveDeadFishes()
    {
        for (int i = _fishes.Count - 1; i >= 0; i--)
        {
            if (_fishes[i].IsDead)
            {
                _fishes.RemoveAt(i);
            }
        }
    }

    private void AddFish()
    {
        if (HasSpace)
        {
            Console.Write("Введите имя рыбы: ");

            string name = Console.ReadLine();

            _fishes.Add(new Fish(name));
        }
        else
        {
            Console.WriteLine("Аквариум полон, нельзя добавить еще одну рыбу.");
        }
    }

    private void RemoveFish()
    {
        Console.Write("Введите имя рыбы для удаления: ");

        string nameToRemove = Console.ReadLine();

        bool fishRemoved = false;
        for (int i = _fishes.Count - 1; i >= 0; i--)
        {
            if (_fishes[i].Name == nameToRemove)
            {
                _fishes.RemoveAt(i);
                fishRemoved = true;
            }
        }

        if (fishRemoved)
        {
            Console.WriteLine($"Рыба {nameToRemove} была удалена из аквариума.");
        }
        else
        {
            Console.WriteLine($"Рыба с именем {nameToRemove} не найдена в аквариуме.");
        }
    }

    private void ShowFishes()
    {
        foreach (Fish fish in _fishes)
        {
            Console.WriteLine($"Имя: {fish.Name}, Возраст: {fish.Age}");
        }
    }
}
