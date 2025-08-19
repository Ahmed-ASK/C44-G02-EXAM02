using ExamSystem.Models.Abstract_Classes;
using ExamSystem.Models.AbstractClasses;
using ExamSystem.UI.Error_Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamSystem.Logic
{
    public static class Validators
    {
        public static bool EmptyAnswerListValidator(ICollection<Answer>? answersList)
        {
            if (answersList == null || !answersList.Any())
            {
                Console.WriteLine(Constants.EmptyAnswerList);
                return false;
            }
            return true;
        }
        public static bool IsValidId(Question question, int id)
        {
            if (question.AnswersList != null && question.AnswersList.Any(e => e.Id == id))
            {
                return true;
            }
            Console.WriteLine(Constants.NotSelectedRightAnswer);
            return false;
        }
        public static bool IsAnswerExists(Question question)
        {
            if (question.AnswersList != null && question.AnswersList.Any())
            {
                return true;
            }
            Console.WriteLine(Constants.EmptyAnswerList);
            return false;
        }
        public static bool IsValidMarks(int marks)
        {
            if (marks > 0)
            {
                return true;
            }
            Console.WriteLine(Constants.InvalidQuestionMarks);
            return false;
        }
        public static bool IsValidBody(string? body)
        {
            if (!string.IsNullOrWhiteSpace(body))
            {
                return true;
            }
            Console.WriteLine(Constants.InvalidQuestionBody);
            return false;
        }
        public static bool IsValidChosenAnswerId(Question question, int chosenAnswerId)
        {
            if (question.AnswersList != null && question.AnswersList.Any(e => e.Id == chosenAnswerId))
            {
                return true;
            }
            Console.WriteLine(Constants.InvalidChosenAnswerId);
            return false;
        }
        public static bool IsValidToAddQuestion(Exam exam)
        {
            if (exam == null)
            {
                Console.WriteLine(Constants.UnexpectedError);
                return false;
            }
            
            if (exam.NumberOfQuestions <= 0)
            {
                Console.WriteLine(Constants.InvalidNumberOfQuestions);
                return false;
            }
            
            if (exam.Questions == null)
            {
                Console.WriteLine(Constants.QuestionsCollectionIsEmpty);
                return false;
            }
            
            if (exam.Questions.Count < exam.NumberOfQuestions)
            {
                return true;
            }
            
            return false;
        }
        public static bool IsValidAnswerText(string? answerText)
        {
            if (!string.IsNullOrWhiteSpace(answerText))
            {
                return true;
            }
            Console.WriteLine("Answer text cannot be empty.");
            return false;
        }
    }
}
