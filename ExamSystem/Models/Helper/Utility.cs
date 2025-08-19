using ExamSystem.Logic;
using ExamSystem.Models.AbstractClasses;
using ExamSystem.Models.Normal_Classes;
using ExamSystem.UI.Messages;
using System;

namespace ExamSystem.Models.Helper
{
    public static class Utility
    {
        public static Question McQuestionCreation() 
        {

            Console.Write(UIMessages.EnterQuestionBody);
            string? body = Console.ReadLine();
            int marks = 0;
            if (Validators.IsValidBody(body))
            {
                Console.Write(UIMessages.EnterQuestionMarks);
                if (!int.TryParse(Console.ReadLine(), out marks) || !Validators.IsValidMarks(marks))
                {
                    marks = 0;
                }
            }
            var question = new McQuestion(body, marks);
            CreatingAnswersForMcQuestion(question);
            Console.Write(UIMessages.EnterRightAnswerId);
            if (int.TryParse(Console.ReadLine(), out int rightAnswerId) && Validators.IsValidId(question, rightAnswerId))
                question.SetRightAnswerId(rightAnswerId);
            return question;
        }
        public static Question TrueFalseQuestionCreation()
        {
            Console.Write(UIMessages.EnterQuestionBody);
            string? body = Console.ReadLine();
            int marks = 0;
            if (Validators.IsValidBody(body))
            {
                Console.Write(UIMessages.EnterQuestionMarks);
                if (!int.TryParse(Console.ReadLine(), out marks) || !Validators.IsValidMarks(marks))
                {
                    marks = 0;
                }
            }
            var question = new TrueFalseQuestion(body, marks);
            Console.Write(UIMessages.EnterRightAnswerId);
            if (int.TryParse(Console.ReadLine(), out int rightAnswerId) && Validators.IsValidId(question, rightAnswerId))
                question.SetRightAnswerId(rightAnswerId);
            return question;
        }
        public static void CreatingAnswersForMcQuestion(Question question)
        {
            Console.WriteLine(UIMessages.CreatingAnswersForThisQuestion);
            if (question?.AnswersList != null)
            {
                int answerIndex = 1;
                foreach (var answer in question.AnswersList)
                {
                    string? answerText;
                    do
                    {
                        Console.Write($"Enter answer {answerIndex}: ");
                        answerText = Console.ReadLine();
                        if (!Validators.IsValidAnswerText(answerText))
                        {
                            Console.WriteLine(UIMessages.InvalidAnswer);
                        }
                    } while (!Validators.IsValidAnswerText(answerText));

                    answer.AnswerText.Append(answerText);
                    answerIndex++;
                }
            }
        }
    }
}
