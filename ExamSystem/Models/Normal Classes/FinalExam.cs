using ExamSystem.Models.Abstract_Classes;
using ExamSystem.Models.AbstractClasses;
using ExamSystem.Models.Enums;
using ExamSystem.Models.Helper;
using ExamSystem.UI.Messages;
using System;
using System.Linq;
using System.Text;

namespace ExamSystem.Models.Normal_Classes
{
    public class FinalExam : Exam
    {
        public FinalExam(int examTime, int numberOfQuestions) : base(ExamType.Final, examTime, numberOfQuestions)
        {
        }

        public override void AddQuestion(Question question)
        {
            if (question != null)
            {
                if (question.Header == QuestionType.TrueFalse || question.Header == QuestionType.MCQ)
                {
                    Questions.Add(question);
                    return;
                }
                else
                {
                    throw new ArgumentException("Final Exam only supports True/False and MCQ questions.");
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(question), "Question cannot be null.");
            }
        }
        
        public override void Show()
        {
            StringBuilder examDisplay = new StringBuilder();
            BuildExamHeader(examDisplay);

            if (Questions == null || !Questions.Any())
            {
                examDisplay.AppendLine(UIMessages.NoQuestionsInExam);
                Console.WriteLine(examDisplay.ToString());
                return;
            }

            int questionNumber = 1;
            foreach (var question in Questions)
            {
                examDisplay.AppendLine($"Question {questionNumber}: {question.Body}");

                if (question.ChosenAnswerId > 0)
                {
                    var chosenAnswer = question.GetAnswerById(question.ChosenAnswerId);
                    examDisplay.AppendLine($"Your Answer: {chosenAnswer}");
                }
                else
                {
                    examDisplay.AppendLine("Your Answer: No answer selected");
                }
                examDisplay.AppendLine();
                questionNumber++;
            }

            examDisplay.AppendLine("FINAL RESULTS:");
            examDisplay.AppendLine(new string('-', 25));
            examDisplay.AppendLine($"Your Score: {CalculateEarnedMarks()}/{CalculateTotalMarks()} points");
            
            double percentage = (double)CalculateEarnedMarks() / CalculateTotalMarks() * 100;
            examDisplay.AppendLine($"Percentage: {percentage:F1}%");

            Console.WriteLine(examDisplay.ToString());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
