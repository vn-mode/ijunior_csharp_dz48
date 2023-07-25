using System;
using System.Collections.Generic;

class Program
{
    private const string CommandPromptMessage = "Введите команду (add, remove, show, quit):";
    private const string UnknownCommandMessage = "Неизвестная команда, попробуйте еще раз.";
    private const string AddCommand = "add";
    private const string RemoveCommand = "remove";
    private const string ShowCommand = "show";
    private const string QuitCommand = "quit";

    static void Main(string[] args)
    {
        var aquarium = new Aquarium(3);
        bool isRunning = true;

        while (isRunning)
        {
            isRunning = ExecuteCommand(aquarium);
            aquarium.Live();
        }
    }

    private static bool ExecuteCommand(Aquarium aquarium)
    {
        Console.WriteLine(CommandPromptMessage);
        var command = Console.ReadLine().Trim().ToLower();

        switch (command)
        {
            case AddCommand:
                aquarium.AddFishPrompt();
                break;

            case RemoveCommand:
                aquarium.RemoveFishPrompt();
                break;

            case ShowCommand:
                aquarium.ShowFishes();
                break;

            case QuitCommand:
                return false;

            default:
                Console.WriteLine(UnknownCommandMessage);
                break;
        }

        return true;
    }
}

public class Fish
{
    private string _name;
    private int _age;

    public Fish(string name)
    {
        _name = name;
        _age = 0;
    }

    public string Name
    {
        get { return _name; }
    }

    public int Age
    {
        get { return _age; }
    }

    public void IncrementAge()
    {
        _age++;
    }

    public bool IsDead()
    {
        return _age > 10; // допустим, рыбка умирает, когда ей становится 10
    }
}

public class Aquarium
{
    private const string FullAquariumMessage = "Аквариум полон, нельзя добавить еще одну рыбу.";
    private const string DeathMessageTemplate = "Рыба {0} умерла от старости.";
    private const string FishInfoTemplate = "Имя: {0}, Возраст: {1}";
    private const string FishRemovedMessageTemplate = "Рыба {0} была удалена из аквариума.";
    private const string FishNotFoundMessageTemplate = "Рыба с именем {0} не найдена в аквариуме.";
    private const string AddFishPromptMessage = "Введите имя рыбы: ";
    private const string RemoveFishPromptMessage = "Введите имя рыбы для удаления: ";

    private readonly int _capacity;
    private List<Fish> _fishes = new List<Fish>();

    public Aquarium(int capacity)
    {
        _capacity = capacity;
    }

    public void AddFishPrompt()
    {
        Console.Write(AddFishPromptMessage);
        var name = Console.ReadLine();
        AddFish(new Fish(name));
    }

    public void RemoveFishPrompt()
    {
        Console.Write(RemoveFishPromptMessage);
        var name = Console.ReadLine();
        RemoveFish(name);
    }

    public void AddFish(Fish fish)
    {
        if (_fishes.Count < _capacity)
        {
            _fishes.Add(fish);
        }
        else
        {
            Console.WriteLine(FullAquariumMessage);
        }
    }

    public void RemoveFish(string name)
    {
        if (_fishes.RemoveAll(fish => fish.Name == name) > 0)
        {
            Console.WriteLine(string.Format(FishRemovedMessageTemplate, name));
        }
        else
        {
            Console.WriteLine(string.Format(FishNotFoundMessageTemplate, name));
        }
    }

    public void Live()
    {
        for (int i = _fishes.Count - 1; i >= 0; i--)
        {
            _fishes[i].IncrementAge();

            if (_fishes[i].IsDead())
            {
                Console.WriteLine(string.Format(DeathMessageTemplate, _fishes[i].Name));
                _fishes.RemoveAt(i);
            }
        }
    }

    public void ShowFishes()
    {
        foreach (var fish in _fishes)
        {
            Console.WriteLine(string.Format(FishInfoTemplate, fish.Name, fish.Age));
        }
    }
}
