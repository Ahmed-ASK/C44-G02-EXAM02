using ExamSystem.Models.Abstract_Classes;
using ExamSystem.Models.Helper;
using ExamSystem.UI.Error_Messages;
using ExamSystem.UI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Models.Normal_Classes
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Exam? ExamOfTheSubject{ get; set; }
        public Subject(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        public void CreateExam()
        {
            Exam exam = null!;
            if (ExamOfTheSubject == null)
            {
                Console.Write("Enter Exam Time: ");
                int examTime;
                while (!int.TryParse(Console.ReadLine(), out examTime) || examTime <= 0)
                {
                    Console.WriteLine(Constants.InvalidExamTime);
                    Console.Write("Enter Exam Time: ");
                }

                Console.Write("Enter Exam Type 1 For Final and 2 for Practical: ");
                bool isValidExamType = false;
                while (!isValidExamType) 
                {
                    int examType;
                    while (!int.TryParse(Console.ReadLine(), out examType) || (examType != 1 && examType != 2))
                    {
                        Console.WriteLine(Constants.InvalidExamType);
                        Console.Write("Enter Exam Type 1 For Final and 2 for Practical: ");
                    }
                    int numberOfQuestions;
                    switch (examType)
                    {
                        case 1:
                            isValidExamType = true;
                            Console.Write("Enter Number Of Questions: ");
                            while (!int.TryParse(Console.ReadLine(), out numberOfQuestions) || numberOfQuestions <= 0)
                            {
                                Console.WriteLine(Constants.InvalidNumberOfQuestions);
                                Console.Write("Enter Number Of Questions: ");
                            }
                            exam = new FinalExam(examTime, numberOfQuestions);
                            for (int i = 0; i < numberOfQuestions; i++)
                            {
                                Console.Write(UIMessages.EnterQuestionType);
                                bool isValidQuestionType = false;
                                while (!isValidQuestionType)
                                {
                                    int questionType;
                                    while (!int.TryParse(Console.ReadLine(), out questionType) || (questionType != 1 && questionType != 2))
                                    {
                                        Console.WriteLine(Constants.InvalidQuestionType);
                                        Console.Write(UIMessages.EnterQuestionType);
                                    }
                                    switch (questionType)
                                    {
                                        case 1:
                                            exam.AddQuestion(Utility.TrueFalseQuestionCreation());
                                            isValidQuestionType = true;
                                            break;
                                        case 2:
                                            exam.AddQuestion(Utility.McQuestionCreation());
                                            isValidQuestionType = true;
                                            break;
                                    }
                                }
                            }
                            break;
                        case 2:
                            isValidExamType = true;
                            Console.Write("Enter Number Of Questions: ");
                            while (!int.TryParse(Console.ReadLine(), out numberOfQuestions) || numberOfQuestions <= 0)
                            {
                                Console.WriteLine(Constants.InvalidNumberOfQuestions);
                                Console.Write("Enter Number Of Questions: ");
                            }
                            exam = new PracticalExam(examTime, numberOfQuestions);
                            for (int i = 0; i < numberOfQuestions; i++)
                            {
                                exam.AddQuestion(Utility.McQuestionCreation());
                            }
                            break;
                    }
                }
                ExamOfTheSubject = exam;
            }
        }

        public void StartExam()
        {
            if (ExamOfTheSubject != null)
            {
                ExamOfTheSubject.Start();
            }
            else
            {
                throw new NullReferenceException(Constants.ExamNotFound);
            }
        }
    }
}
