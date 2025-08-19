using ExamSystem.Logic;
using ExamSystem.Models.AbstractClasses;
using ExamSystem.Models.Enums;
using ExamSystem.UI.Error_Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.Models.Abstract_Classes
{
    public abstract class Exam
    {
        
        public ExamType TypeOfExam{ get; set; }
        // TODO : Add Validation for ExamTime and NumberOfQuestions
        private int examTime;
        private int numberOfQuestions;

        public int ExamTime
        {
            get => examTime;  
            set
            {
                if (value <= 0)
                    throw new ArgumentException(Constants.InvalidExamTime);
                examTime = value; 
            }
        }

        public int NumberOfQuestions
        {
            get => numberOfQuestions;  
            set
            {
                if (value <= 0)
                    throw new ArgumentException(Constants.InvalidNumberOfQuestions);
                numberOfQuestions = value;  
            }
        }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public Exam(ExamType typeOfExam, int examTime, int numberOfQuestions)
        {
            TypeOfExam = typeOfExam;
            ExamTime = examTime;
            NumberOfQuestions = numberOfQuestions;
        }
        // Why Abstract?
        // Final Exam => can have TrueFalse & MultipleChoice Questions
        // Midterm Exam => can have MultipleChoice Questions only
        public abstract void AddQuestion(Question question);
        public abstract void Show();
        
        protected int CalculateEarnedMarks()
        {
            int earnedMarks = 0;
            foreach (var question in Questions)
            {
                if (question.ChosenAnswerId == question.GetRightAnswer().Id)
                {
                    earnedMarks += question.Marks;
                }
            }
            return earnedMarks;
        }
        
        protected int CalculateTotalMarks()
        {
            int totalMarks = 0;
            foreach (var question in Questions)
            {
                totalMarks += question.Marks;
            }
            return totalMarks;
        }
        
        protected void BuildExamHeader(StringBuilder display)
        {
            display.AppendLine($"EXAM TYPE: {TypeOfExam}");
            display.AppendLine($"EXAM TIME: {ExamTime} minutes");
            display.AppendLine($"NUMBER OF QUESTIONS: {NumberOfQuestions}");
            display.AppendLine();
        }

        public void Start()
        {
            if (Questions.Count == 0)
            {
                throw new InvalidOperationException("The Exam's Questions Is Not Initialized (Developer Side Issue)");
            }
            
            Console.Clear();
            Console.WriteLine($"Starting {TypeOfExam} Exam");
            Console.WriteLine($"Time: {ExamTime} minutes | Questions: {NumberOfQuestions}");
            Console.WriteLine();
            Console.WriteLine("Press any key to begin...");
            Console.ReadKey();
            
            int questionNumber = 1;
            foreach (var question in Questions)
            {
                Console.Clear();
                Console.WriteLine($"Question {questionNumber} of {NumberOfQuestions}");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine(question.DisplayQuestion());
                Console.Write("Your answer: ");
                
                if (int.TryParse(Console.ReadLine(), out int chosenAnswerId) && 
                    Validators.IsValidId(question, chosenAnswerId))
                {
                    question.SetChosenAnswerId(chosenAnswerId);
                    Console.WriteLine($"Answer {chosenAnswerId} selected.");
                    
                    if (questionNumber < NumberOfQuestions)
                    {
                        Console.WriteLine("Press any key for next question...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    throw new InvalidOperationException(Constants.InvalidChosenAnswerId);
                }
                questionNumber++;
            }
            
            Console.Clear();
            Console.WriteLine("Exam completed! Calculating results...");
            Console.WriteLine();
            Show();
        }
    }
}
