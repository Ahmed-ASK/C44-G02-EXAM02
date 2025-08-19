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
        /// <summary>
        /// this method will be called to add a question to the exam in the <see cref="Subject.CreateExam"> method.
        /// </summary>
        /// <param name="question"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
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
        /// <summary>
        /// This method displays the result of the final exam without showing the right answers for the user making it safe to use in the final exam without the abillity to cheat .
        /// </summary>
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
                    if (chosenAnswer != null)
                        examDisplay.AppendLine($"Your Answer: {chosenAnswer}");
                    else
                        examDisplay.AppendLine("Your Answer: No answer selected");
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
            
            int earnedMarks = CalculateEarnedMarks();
            int totalMarks = CalculateTotalMarks();
            double percentage = totalMarks > 0 ? (double)earnedMarks / totalMarks * 100.0 : 0.0;
            examDisplay.AppendLine($"Percentage: {percentage:F1}%");

            Console.WriteLine(examDisplay.ToString());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
