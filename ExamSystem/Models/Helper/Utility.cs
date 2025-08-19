using ExamSystem.Logic;
using ExamSystem.Models.AbstractClasses;
using ExamSystem.Models.Normal_Classes;
using ExamSystem.UI.Error_Messages;
using ExamSystem.UI.Messages;
using System;

namespace ExamSystem.Models.Helper
{
    public static class Utility
    {
        /// <summary>
        /// This Method is used to create a Multiple Choice Question. and from inside this method, the user is prompted to enter the question body, marks, and answers.
        /// by calling the <see cref="CreatingAnswersForMcQuestion(Question)"> method, the user is prompted to enter the answers for the question.
        /// allowing for easily mentain the code and add more features in the future.
        /// </summary>
        /// <returns></returns>
        public static Question McQuestionCreation() 
        {
            Console.Write(UIMessages.EnterQuestionBody);
            string? body = Console.ReadLine();

            int marks;
            Console.Write(UIMessages.EnterQuestionMarks);
            while (!int.TryParse(Console.ReadLine(), out marks) || !Validators.IsValidMarks(marks))
            {
                Console.WriteLine(Constants.InvalidQuestionMarks);
                Console.Write(UIMessages.EnterQuestionMarks);
            }

            var question = new McQuestion(body, marks);
            CreatingAnswersForMcQuestion(question);
            Console.Write(UIMessages.EnterRightAnswerId);
            if (int.TryParse(Console.ReadLine(), out int rightAnswerId) && Validators.IsValidId(question, rightAnswerId))
                question.SetRightAnswerId(rightAnswerId);
            return question;
        }
        /// <summary>
        /// this method is used to create a True/False Question.
        /// it will make the user enter the question body, marks, and right answer id.
        /// </summary>
        /// <returns></returns>
        public static Question TrueFalseQuestionCreation()
        {
            Console.Write(UIMessages.EnterQuestionBody);
            string? body = Console.ReadLine();

            int marks;
            Console.Write(UIMessages.EnterQuestionMarks);
            while (!int.TryParse(Console.ReadLine(), out marks) || !Validators.IsValidMarks(marks))
            {
                Console.WriteLine(Constants.InvalidQuestionMarks);
                Console.Write(UIMessages.EnterQuestionMarks);
            }

            var question = new TrueFalseQuestion(body, marks);
            Console.Write(UIMessages.EnterRightAnswerId);
            if (int.TryParse(Console.ReadLine(), out int rightAnswerId) && Validators.IsValidId(question, rightAnswerId))
                question.SetRightAnswerId(rightAnswerId);
            return question;
        }
        /// <summary>
        /// this is a healper method that will create answers for a Multiple Choice Question.
        /// </summary>
        /// <param name="question"></param>
        private static void CreatingAnswersForMcQuestion(Question question)
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
