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
    public class PracticalExam : Exam
    {
        public PracticalExam(int examTime, int numberOfQuestions)
            : base(ExamType.Practical, examTime, numberOfQuestions)
        {
        }

        public override void AddQuestion(Question question)
        {
            if (question != null && Questions.Count < NumberOfQuestions)
            {
                if (question.Header == QuestionType.MCQ)
                {
                    Questions.Add(question);
                    return;
                }
                else
                {
                    throw new ArgumentException("Practical Exam only supports MCQ questions.");
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

            int earnedMarks = CalculateEarnedMarks();
            int totalMarks = CalculateTotalMarks();

            examDisplay.AppendLine("FINAL RESULTS:");
            examDisplay.AppendLine(new string('-', 25));
            examDisplay.AppendLine($"Your Score: {earnedMarks}/{totalMarks} points");
            
            double percentage = (double)earnedMarks / totalMarks * 100;
            examDisplay.AppendLine($"Percentage: {percentage:F1}%");
            examDisplay.AppendLine();
            
            examDisplay.AppendLine("DETAILED REVIEW:");
            examDisplay.AppendLine(new string('-', 25));
            
            int questionNumber = 1;
            foreach (var question in Questions)
            {
                examDisplay.AppendLine($"Q{questionNumber}: {question.Body}");
                
                if (question.ChosenAnswerId > 0)
                {
                    var chosenAnswer = question.GetAnswerById(question.ChosenAnswerId);
                    examDisplay.AppendLine($"Your Answer: {chosenAnswer}");
                }
                else
                {
                    examDisplay.AppendLine("Your Answer: No answer selected");
                }
                
                var rightAnswer = question.GetRightAnswer();
                examDisplay.AppendLine($"Correct Answer: {rightAnswer}");

                if (question.ChosenAnswerId == question.GetRightAnswer().Id)
                {
                    examDisplay.AppendLine("✓ CORRECT");
                }
                else
                {
                    examDisplay.AppendLine("✗ INCORRECT");
                }
                
                examDisplay.AppendLine();
                questionNumber++;
            }
            
            Console.WriteLine(examDisplay.ToString());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}