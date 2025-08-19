using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.UI.Messages
{
    public static class UIMessages
    {
        public const string EnterQuestionType = "Enter Question Type (1 for True/False, 2 for MCQ): ";
        public const string EnterQuestionBody = "Enter Question Body: ";
        public const string EnterQuestionMarks = "Enter Marks: ";
        public const string EnterRightAnswerId = "Enter Right Answer Id: ";
        public const string CreatingAnswersForThisQuestion = "Creating Answers For MCQ";
        public const string QuestionCreatedSuccessfully = "Question Created Successfully!";
        public const string Separator = "==================================================";
        public const string EnterNumberOfQuestions = "Enter Number Of Questions: ";
        public const string InvalidAnswer = "Invalid Answer. Please try again.";
        public const string NoQuestionsInExam = "No questions in the exam.";
    }
}
