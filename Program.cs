using System;
using System.Collections.Generic;
using System.Linq;
using TodoListApp;

namespace TodoListApp
{
    public class Program
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        static Random random = new Random();

        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== TO-DO LIST ===");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View All Tasks");
                Console.WriteLine("3. Mark Task as Completed");
                Console.WriteLine("4. View Completed Tasks");
                Console.WriteLine("5. View Pending Tasks");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ViewTasks();
                        break;
                    case "3":
                        MarkTaskAsCompleted();
                        break;
                    case "4":
                        ViewCompletedTasks();
                        break;
                    case "5":
                        ViewPendingTasks();
                        break;
                    case "6":
                        Console.WriteLine("Exiting... Stay productive!");
                        return;
                    case null:
                        Console.WriteLine("No input detected. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void AddTask()
        {
            Console.Write("Enter task description: ");
            string? description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Description cannot be empty. Task not added.");
                Console.WriteLine("Press Enter to return to the menu.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter due date (yyyy-mm-dd hh:mm): ");
            string? dueDateInput = Console.ReadLine();
            if (dueDateInput == null || !DateTime.TryParse(dueDateInput, out DateTime dueDate))
            {
                Console.WriteLine("Invalid date format. Task not added.");
            }
            else
            {
                tasks.Add(new TaskItem(description, dueDate));
                Console.WriteLine("Task added successfully!");
            }

            Console.WriteLine("Press Enter to return to the menu.");
            Console.ReadLine();
        }

        // Modified to accept a parameter to skip waiting for Enter
        static void ViewTasks(bool waitForEnter = true)
        {
            Console.WriteLine("\nAll Tasks:");
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks added.");
            }
            else
            {
                int index = 1;
                foreach (var task in tasks)
                {
                    Console.WriteLine($"{index++}. [{(task.IsCompleted ? "X" : " ")}] {task.Description} - Due: {task.DueDate:g}" +
                                      (DateTime.Now > task.DueDate && !task.IsCompleted ? " (Overdue)" : ""));
                }
            }

            ShowQuote();
            if (waitForEnter)
            {
                Console.WriteLine("\nPress Enter to return to the menu.");
                Console.ReadLine();
            }
        }

        static void MarkTaskAsCompleted()
        {
            ViewTasks(false); // Don't wait for Enter here
            Console.Write("\nEnter task number to mark as completed: ");
            string? input = Console.ReadLine();
            if (input != null && int.TryParse(input, out int index) && index >= 1 && index <= tasks.Count)
            {
                tasks[index - 1].IsCompleted = true;
                Console.WriteLine("Task marked as completed.");
            }
            else
            {
                Console.WriteLine("Invalid task number.");
            }

            Console.WriteLine("Press Enter to return to the menu.");
            Console.ReadLine();
        }

        static void ViewCompletedTasks()
        {
            Console.WriteLine("\nCompleted Tasks:");
            var completedTasks = tasks.FindAll(t => t.IsCompleted);
            if (completedTasks.Count == 0)
            {
                Console.WriteLine("No completed tasks.");
            }
            else
            {
                int index = 1;
                foreach (var task in completedTasks)
                {
                    Console.WriteLine($"{index++}. {task.Description} - Completed - Due: {task.DueDate:g}");
                }
            }

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }

        static void ViewPendingTasks()
        {
            Console.WriteLine("\nPending Tasks:");
            var pendingTasks = tasks.FindAll(t => !t.IsCompleted);
            if (pendingTasks.Count == 0)
            {
                Console.WriteLine("No pending tasks.");
            }
            else
            {
                int index = 1;
                foreach (var task in pendingTasks)
                {
                    Console.WriteLine($"{index++}. {task.Description} - Due: {task.DueDate:g}" +
                                      (DateTime.Now > task.DueDate ? " (Overdue)" : ""));
                }
            }

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }

        static void ShowQuote()
        {
            string[] quotes =
            {
                "Don’t count the days, make the days count.",
                "Push yourself, because no one else is going to do it for you.",
                "Success doesn’t just find you. You have to go out and get it.",
                "Great things never come from comfort zones.",
                "Dream it. Wish it. Do it."
            };

            string quote = quotes[random.Next(quotes.Length)];
            Console.WriteLine($"\n💡 Motivation: \"{quote}\"");
        }
    }
}