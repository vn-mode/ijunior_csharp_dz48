using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        bool isRunning = true;
        Aquarium aquarium = new Aquarium(3);

        while (isRunning)
        {
            aquarium.Interact();
            aquarium.Live();
            aquarium.RemoveDeadFishes();
        }
    }
}

public class Fish
{
    private const int MaxAge = 10;
    private int _age;

    public Fish(string name)
    {
        Name = name;
        _age = 0;
    }

    public string Name { get; private set; }
    public int Age => _age;
    public bool IsDead => (_age > MaxAge);

    public void IncrementAge()
    {
        _age++;
    }
}

public class Aquarium
{
    private readonly int _capacity;
    private List<Fish> fishes;

    public Aquarium(int capacity)
    {
        _capacity = capacity;
        fishes = new List<Fish>();
    }

    public bool HasSpace => fishes.Count < _capacity;

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
                AddFish();
                break;

            case RemoveCommand:
                RemoveFish();
                break;

            case ShowCommand:
                ShowFishes();
                break;

            case QuitCommand:
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Неизвестная команда, попробуйте еще раз.");
                break;
        }
    }

    public void Live()
    {
        foreach (Fish fish in fishes)
        {
            fish.IncrementAge();
        }
    }

    public void RemoveDeadFishes()
    {
        for (int i = fishes.Count - 1; i >= 0; i--)
        {
            if (fishes[i].IsDead)
            {
                fishes.RemoveAt(i);
            }
        }
    }

    private void AddFish()
    {
        if (HasSpace)
        {
            Console.Write("Введите имя рыбы: ");

            string name = Console.ReadLine();

            fishes.Add(new Fish(name));
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

        if (RemoveFish(nameToRemove))
        {
            Console.WriteLine($"Рыба {nameToRemove} была удалена из аквариума.");
        }
        else
        {
            Console.WriteLine($"Рыба с именем {nameToRemove} не найдена в аквариуме.");
        }
    }

    private bool RemoveFish(string name)
    {
        foreach (Fish fish in fishes)
        {
            if (fish.Name == name)
            {
                fishes.Remove(fish);
                return true;
            }
        }
        return false;
    }

    private void ShowFishes()
    {
        foreach (Fish fish in fishes)
        {
            Console.WriteLine($"Имя: {fish.Name}, Возраст: {fish.Age}");
        }
    }
}