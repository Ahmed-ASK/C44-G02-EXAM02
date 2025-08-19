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

        /// <summary>
        /// This method creates an exam for the subject.
        /// Inside this method, the user is prompted to enter the exam time, type, and number of questions.
        /// After choosing the exam type, the user is prompted to enter the question type for each question
        /// if the exam type is Final Exam, the user can choose between True/False or Multiple Choice questions.
        /// and for each MC Question he must provide the marks for that question answers and right answer id .
        /// for True/False questions, the user is prompted to enter the question body and the right answer.
        /// 
        /// For Practical Exam, the user can only choose Multiple Choice questions.
        /// And must provide the marks for that question , answers , and right answer id.
        /// 
        /// this method will create the exam and assign it to the <see cref="ExamOfTheSubject"> property.
        /// and depending on the exam type, it will validate the question type and add the questions to the exam.
        /// using the <see cref="Exam.AddQuestion(Question)"> method.
        /// which depending on the user choices will call the healper method for creating each question depending on the type he chosen
        /// </summary>
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
                                            exam.AddQuestion(Utility.TrueFalseQuestionCreation()); // this will call the helper method to create a True/False question
                                            isValidQuestionType = true;
                                            break;
                                        case 2:
                                            exam.AddQuestion(Utility.McQuestionCreation()); // this will call the helper method to create a Multiple Choice question
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
        /// <summary>
        /// This method starts the exam for the subject , but after checking if there is an exam existing in that subject or not .
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
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
