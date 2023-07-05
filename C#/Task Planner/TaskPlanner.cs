using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

class TaskPlanner
{
    static void AddCategory(Collection collection)
    {
        string categoryName;

        Console.WriteLine("Enter category name to add: ");
        categoryName = Console.ReadLine();
        Category newCategory = new Category(categoryName);
        collection.AddCategory(newCategory);

        SelectMenu(collection);
    }

    static void DeleteCategory(Collection collection)
    {
        string categoryName;
        Console.WriteLine("Enter category name to delete: ");
        categoryName = Console.ReadLine();
        collection.DeleteCategory(collection.FindCategory(categoryName));
    }

    static void Print(Collection collection)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(new string(' ', 12) + "CATEGORIES");
        Console.WriteLine(new string('-', 50));
        List<Category> categories = collection.GetCategoriesWhole();

        foreach (Category category in collection.GetCategories())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{category.Name}");

            int value = 0;
            foreach (string tasks1 in collection.GetTasks(category))
            {
                List<bool> priorityList = new List<bool>();

                int i = 0;
                foreach (bool priority in collection.GetPriority(category))
                {
                    priorityList.Add(priority);
                }

                if (priorityList[value] == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{value}. {tasks1}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{value}.{tasks1}");
                }

                value++;
                i++;
            }
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(new string('-', 50));
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    static void AddTask(Collection collection)
    {
        string categoryName;
        string task;

        Console.ResetColor();
        Print(collection);
        Console.WriteLine("\nWhich category do you want to ADD a new task: ");
        Console.Write(">> ");

        categoryName = Console.ReadLine();
        Console.WriteLine("Describe your task below (max. 30 symbols).");
        Console.Write(">> ");
        task = Console.ReadLine();

        if (task.Length > 30)
        {
            task = task.Substring(0, 30);
        }

        collection.AddTask(collection.FindCategory(categoryName), task);
        SelectMenu(collection);
    }

    static void DeleteTask(Collection collection)
    {
        string categoryName;
        int taskIndex;

        Console.ResetColor();
        Print(collection);
        Console.WriteLine("\nWhich category do you want to DELETE a new task. ");
        Console.Write(">> ");

        categoryName = Console.ReadLine();
        Console.WriteLine("Select task to DELETE by index");
        Console.Write(">> ");
        taskIndex = Convert.ToInt32(Console.ReadLine());

        collection.DeleteTask(collection.FindCategory(categoryName), taskIndex);
        SelectMenu(collection);
    }

    static void MoveTask(Collection collection)
    {
        int taskIndex;
        string orgCategoryName;

        Category destCategory;
        string destCategoryName;
        int destCategoryIndex;

        Console.ResetColor();
        Print(collection);

        Console.WriteLine("\nSelect original category name");
        Console.Write(">> ");
        orgCategoryName = Console.ReadLine();

        Console.WriteLine("\nSelect Destination category name");
        Console.Write(">> ");
        destCategoryName = Console.ReadLine();

        Console.WriteLine("\n Select which task to move, by index ");
        Console.Write(">> ");

        taskIndex = Convert.ToInt32(Console.ReadLine());
        collection.MoveTask(collection.FindCategory(orgCategoryName), collection.FindCategory(destCategoryName), taskIndex);

        SelectMenu(collection);
    }

    static void HighlightImportantTask(Collection collection)
    {
        string categoryName;
        int taskIndex;

        Console.ResetColor();
        Print(collection);
        Console.WriteLine("\n In which category do you want to HIGHLIGHT a task: ");
        Console.Write(">> ");

        categoryName = Console.ReadLine();
        Console.WriteLine("\nSelect task to HIGHLIGHT by Index: ");
        Console.Write(">> ");

        taskIndex = Convert.ToInt32(Console.ReadLine());

        collection.HighlightTask(collection.FindCategory(categoryName), taskIndex);
        SelectMenu(collection);
    }


    static void SetPriority(Collection collection)
    {
        string categoryName;
        int orgIndex;
        int destIndex;

        Console.ResetColor();
        Print(collection);
        Console.WriteLine("\n In which category do you want to PRIORITISE a task.");
        Console.Write(">> ");
        categoryName = Console.ReadLine();

        Console.WriteLine("\nSelect ORIGIN task index. ");
        Console.Write(">> ");
        orgIndex = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("\nSelect DESTINATION task.");
        Console.Write(">> ");
        destIndex = Convert.ToInt32(Console.ReadLine());

        collection.SetPriority(collection.FindCategory(categoryName), orgIndex, destIndex);
        SelectMenu(collection);

    }

    static void SetDueDate(Collection collection)
    {
        string categoryName;
        string dueDate;
        int taskIndex;


        Console.ResetColor();
        Print(collection);
        Console.WriteLine("\n In which category do you want to SET A DUE DATE");
        Console.Write(">> ");
        categoryName = Console.ReadLine();

        Console.WriteLine("\nSelect task to by Index");
        Console.Write(">> ");
        taskIndex = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("\nEnter the new due date");
        Console.Write(">> ");
        dueDate = Console.ReadLine();

        collection.SetDueDate(collection.FindCategory(categoryName), taskIndex, dueDate);
        SelectMenu(collection);
    }

    static void Main()
    {
        Collection collection = new Collection();

        Category personalList = new Category("Personal");
        Category familyList = new Category("Family");
        Category workList = new Category("Work");

        collection.AddCategory(personalList);
        collection.AddCategory(familyList);
        collection.AddCategory(workList);

        collection.AddTask(collection.FindCategory("Personal"), "Play cricket");
        collection.AddTask(collection.FindCategory("Family"), "Wash clothes");
        collection.AddTask(collection.FindCategory("Work"), "Wake up");

        collection.AddTask(collection.FindCategory("Personal"), "Go to gym");
        collection.AddTask(collection.FindCategory("Personal"), "Fix computer");


        SelectMenu(collection);
    }


    static void SelectMenu(Collection collection)
    {

        while (true)
        {
            Console.WriteLine("\nWhat do you wish to do?");
            Console.WriteLine("0. Add category");
            Console.WriteLine("1. Delete Category");
            Console.WriteLine("2. Add task");
            Console.WriteLine("3. Delete task");
            Console.WriteLine("4. Move task category");
            Console.WriteLine("5. Highlight important task");
            Console.WriteLine("6. Change priority within category");
            Console.WriteLine("7. Set due date");
            Console.WriteLine("8. View Task Planner");

            Console.Write(">> ");
            int input = Convert.ToInt32(Console.ReadLine());

            switch (input)
            {
                case 0:
                    Console.WriteLine("");
                    AddCategory(collection);
                    break;

                case 1:
                    Console.WriteLine("");
                    DeleteCategory(collection);
                    break;

                case 2:
                    Console.WriteLine("");
                    AddTask(collection);
                    break;

                case 3:
                    Console.WriteLine("");
                    DeleteTask(collection);
                    break;

                case 4:
                    Console.WriteLine("");
                    MoveTask(collection);
                    break;

                case 5:
                    Console.WriteLine("");
                    HighlightImportantTask(collection);
                    break;

                case 6:
                    Console.WriteLine("");
                    SetPriority(collection);
                    break;

                case 7:
                    Console.WriteLine("");
                    SetDueDate(collection);
                    break;

                case 8:
                    Console.WriteLine("");
                    Print(collection);
                    break;
            }

        }
    }
}


class Collection
{
    private List<Category> _category = new List<Category>();

    public void AddCategory(Category category)
    {
        this._category.Add(category);
        //      Console.WriteLine($"Added new category: {category.Name}");
        //    Console.WriteLine($"{this._category.Count}");
    }

    public Category FindCategory(string name)
    {
        foreach (Category category in this._category)
        {
            if (category.Name == name)
            {
                //  Console.WriteLine($"Returning category {category.Name}");
                return category;
            }
        }
        Console.WriteLine("No category found with matching name");
        return null;
    }

    public void AddTask(Category category, string task)
    {
        category.AddTask(task);
        Console.WriteLine($"Added task:{task} to category:{category.Name}.\n");
    }

    public void DeleteTask(Category category, int index)
    {
        category.DeleteTaskByIndex(index);
        //     Console.WriteLine($"Delete completed index {index}\n");
    }

    public void DeleteCategory(Category category)
    {
        this._category.Remove(category);
        Console.WriteLine("Removed Category");
        // Console.WriteLine($"{this._category.Count}");
    }

    public List<Category> GetCategories()
    {
        return this._category;
    }

    public List<bool> GetPriority(Category category)
    {
        return category.Priorities;
    }

    public List<string> GetTasks(Category category)
    {
        List<string> dueDates = category.DueDates;
        List<string> tasks = category.Tasks;
        List<string> returnList = new List<string>();

        for (int i = 0; i < tasks.Count; i++)
        {
            returnList.Add($"{tasks[i].PadRight(30)} {dueDates[i]}");
        }
        return returnList;
    }


    public void MoveTask(Category orgCategory, Category desCategory, int index)
    {
        string orgTask = orgCategory.GetTaskByIndex(index);
        orgCategory.DeleteTaskByIndex(index);
        desCategory.AddTask(orgTask);

        Console.WriteLine($"Moved {orgTask} to {desCategory.Name}.");
    }

    public void HighlightTask(Category category, int index)
    {
        category.SetPriority(index);
        Console.WriteLine($"Set {index} colour to red.");
    }

    public void SetPriority(Category category, int orgIndex, int destIndex)
    {
        string orgTask = category.GetTaskByIndex(orgIndex);
        string orgDueDate = category.getDueDateByIndex(orgIndex);
        bool orgPriority = category.getPriorityByIndex(orgIndex);

        string destTask = category.GetTaskByIndex(destIndex);
        string destDueDate = category.getDueDateByIndex(destIndex);
        bool destPriority = category.getPriorityByIndex(destIndex);

        category.DeleteTaskByIndex(orgIndex);

        if (destIndex != 0)
        {
            destIndex--;
        }

        category.DeleteTaskByIndex((destIndex));
        category.AddTaskByIndex(orgTask, orgDueDate, orgPriority, destIndex);
        category.AddTaskByIndex(destTask, destDueDate, destPriority, orgIndex);
        Console.WriteLine("Move Successful");
    }

    public void SetDueDate(Category category, int taskIndex, string dueDate)
    {
        category.SetDueDate(dueDate, taskIndex);
        Console.WriteLine("New due date set");
    }

    public List<Category> GetCategoriesWhole()
    {
        return this._category;
    }

}

class Category
{
    private string _category;
    private List<string> _tasks;
    private List<string> _dueDate;
    private List<bool> _priority;

    public string Name { get { return this._category; } }
    public List<string> Tasks { get { return this._tasks; } }

    public List<string> DueDates { get { return this._dueDate; } }

    public List<bool> Priorities { get { return this._priority; } }

    public string GetTaskByIndex(int index)
    {
        return this._tasks[index];
    }

    public Category(string category)
    {
        this._category = category;
        this._tasks = new List<string>();
        this._dueDate = new List<string>();
        this._priority = new List<bool>();

        this._dueDate.Add("01/07/2023");
        this._priority.Add(false);
    }

    public void AddTask(string task)
    {
        this._tasks.Add(task);
        this._dueDate.Add("01/07/2023");
        this._priority.Add(false);
    }

    public void DeleteTaskByIndex(int index)
    {
        this._tasks.RemoveAt(index);
        this._priority.RemoveAt(index);
        this._dueDate.RemoveAt(index);
    }

    public void SetPriority(int index)
    {
        this._priority[index] = true;
    }

    public string getDueDateByIndex(int index)
    {
        return this._dueDate[index];
    }

    public bool getPriorityByIndex(int index)
    {
        return this._priority[index];
    }
    public void AddTaskByIndex(string task, string dueDate, bool priority, int index)
    {
        this._tasks.Insert(index, task);
        this._dueDate.Insert(index, dueDate);
        this._priority.Insert(index, priority);
    }

    public void SetDueDate(string dueDate, int index)
    {
        this._dueDate[index] = dueDate;
    }
}