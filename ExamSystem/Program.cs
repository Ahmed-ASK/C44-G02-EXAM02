using ExamSystem.Models.AbstractClasses;
using ExamSystem.Models.Normal_Classes;
using ExamSystem.Models.Abstract_Classes;
using ExamSystem.Models.Enums;
using ExamSystem.UI.Error_Messages;

namespace ExamSystem
{
    internal class Program
    {
        private static List<Subject> subjects = new List<Subject>();
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("═══════════════════════════════════");
                Console.WriteLine("     WELCOME TO EXAM SYSTEM");
                Console.WriteLine("═══════════════════════════════════");
                Console.WriteLine("1. Create Subject");
                Console.WriteLine("2. Subject List");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.Write("Choose an option: ");
                
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CreateSubject();
                            break;
                        case 2:
                            ShowSubjectsList();
                            break;
                        case 3:
                            Console.WriteLine("\nExiting the system. Goodbye!");
                            Console.WriteLine("Press any key to exit...");
                            Console.ReadKey();
                            return;
                        default:
                            Console.WriteLine("\nInvalid choice, please try again.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please enter a number.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
        private static void CreateSubject()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════");
            Console.WriteLine("        CREATE NEW SUBJECT");
            Console.WriteLine("═══════════════════════════════════");
            
            Console.Write("Enter Subject ID: ");
            int.TryParse(Console.ReadLine(), out int id);
            
            // Check if ID already exists
            if (subjects.Any(s => s.Id == id))
            {
                Console.WriteLine($"\nError: Subject with ID {id} already exists!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Enter Subject Name: ");
            string? name = Console.ReadLine();
            
            Subject subject = new Subject(id, name);
            subjects.Add(subject);
            
            Console.WriteLine($"\n✅ Subject '{subject.Name}' with ID {subject.Id} created successfully!");
            Console.WriteLine("\nWould you like to:");
            Console.WriteLine("1. Enter this subject now");
            Console.WriteLine("2. Return to main menu");
            Console.Write("\nChoose option: ");
            
            if (int.TryParse(Console.ReadLine(), out int choice) && choice == 1)
            {
                NavigateToSubject(subject);
            }
        }
        
        private static void ShowSubjectsList()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════");
            Console.WriteLine("          SUBJECTS LIST");
            Console.WriteLine("═══════════════════════════════════");
            
            if (!subjects.Any())
            {
                Console.WriteLine("No subjects created yet.");
                Console.WriteLine("\nPress any key to return to main menu...");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("Available Subjects:");
            Console.WriteLine();
            
            for (int i = 0; i < subjects.Count; i++)
            {
                var subject = subjects[i];
                string examStatus = subject.ExamOfTheSubject != null ? "✅ Has Exam" : "❌ No Exam";
                Console.WriteLine($"{i + 1}. {subject.Name} (ID: {subject.Id}) - {examStatus}");
            }
            
            Console.WriteLine($"{subjects.Count + 1}. Return to Main Menu");
            Console.WriteLine();
            Console.Write("Choose a subject to navigate to: ");
            
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice > 0 && choice <= subjects.Count)
                {
                    NavigateToSubject(subjects[choice - 1]);
                }
                else if (choice == subjects.Count + 1)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("\nInvalid choice!");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    ShowSubjectsList();
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input! Please enter a number.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                ShowSubjectsList(); 
            }
        }
        
        private static void NavigateToSubject(Subject subject)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("═══════════════════════════════════");
                Console.WriteLine($"      SUBJECT: {subject.Name?.ToUpper()}");
                Console.WriteLine("═══════════════════════════════════");
                Console.WriteLine($"Subject ID: {subject.Id}");
                Console.WriteLine($"Exam Status: {(subject.ExamOfTheSubject != null ? "✅ Ready" : "❌ Not Created")}");
                Console.WriteLine();
                Console.WriteLine("1. Create Exam");
                Console.WriteLine("2. Start Exam");
                Console.WriteLine("3. Return to Subject List");
                Console.WriteLine("4. Return to Main Menu");
                Console.WriteLine();
                Console.Write("Enter your choice: ");
                
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            if (subject.ExamOfTheSubject != null)
                            {
                                Console.WriteLine("⚠️ This subject already has an exam!");
                                Console.WriteLine("Do you want to replace it? (y/n): ");
                                string? response = Console.ReadLine()?.ToLower();
                                if (response != "y" && response != "yes")
                                {
                                    Console.WriteLine("Exam creation cancelled.");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            subject.CreateExam();
                            Console.WriteLine("\n✅ Exam created successfully!");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case 2:
                            if (subject.ExamOfTheSubject != null)
                            {
                                subject.ExamOfTheSubject.Start();
                            }
                            else
                            {
                                Console.WriteLine("\n❌ " + Constants.ExamNotFound);
                                Console.WriteLine("Please create an exam first.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            break;
                        case 3:
                            ShowSubjectsList();
                            return;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("\nInvalid choice, please try again.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please enter a number.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

    }
}
