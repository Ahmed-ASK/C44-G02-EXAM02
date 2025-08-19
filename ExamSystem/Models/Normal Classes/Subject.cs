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
                int.TryParse(Console.ReadLine(), out int examTime);

                Console.Write("Enter Exam Type 1 For Final and 2 for Practical: ");
                bool isValidExamType = false;
                while (!isValidExamType) 
                {
                    int.TryParse(Console.ReadLine(), out int examType);
                    switch (examType)
                    {
                        case 1:
                            isValidExamType = true;
                            Console.Write("Enter Number Of Questions: ");
                            int.TryParse(Console.ReadLine(), out int numberOfQuestions);
                            exam = new FinalExam(examTime, numberOfQuestions);
                            for (int i = 0; i < numberOfQuestions; i++)
                            {
                                Console.Write(UIMessages.EnterQuestionType);
                                bool isValidQuestionType = false;
                                while (!isValidQuestionType)
                                {
                                    int.TryParse(Console.ReadLine(), out int questionType);
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
                                        default:
                                            Console.WriteLine(Constants.InvalidQuestionType);
                                            break;
                                    }
                                }
                            }
                            break;
                        case 2:
                            isValidExamType = true;
                            Console.Write("Enter Number Of Questions: ");
                            int.TryParse(Console.ReadLine(), out numberOfQuestions);
                            exam = new PracticalExam(examTime, numberOfQuestions);
                            for (int i = 0; i < numberOfQuestions; i++)
                            {
                                exam.AddQuestion(Utility.McQuestionCreation());
                            }
                            break;
                        default:
                            Console.WriteLine(Constants.InvalidExamType);
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
