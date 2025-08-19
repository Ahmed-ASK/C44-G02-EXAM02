using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.UI.Error_Messages
{
    public static class Constants
    {
        public const string EmptyAnswerList = "The Answer List Is Empty Or Contain less than 2 Answers";
        public const string NotSelectedRightAnswer = "You Have Not Selected The Right Answer";
        public const string InvalidTrueFalseQuestionRightAnswerId = "The Right Answer Id Is Invalid, It Should Be 1 or 2";
        public const string InvalidMcQuestionRightAnswerId = "The Right Answer Id Is Invalid, It Should Be Between 1 and 4";
        public const string InvalidQuestionType = "The Question Type Is Invalid";
        public const string InvalidQuestionBody = "The Question Body Is Invalid, It Should Not Be Null Or Empty";
        public const string InvalidQuestionMarks = "The Question Marks Is Invalid, It Should Be Greater Than 0";
        public const string InvalidNumberOfQuestions = "The Number Of Questions Is Invalid, It Should Be Greater Than 0";
        public const string InvalidChosenAnswerId = "The Chosen Answer Id Is Invalid, It Should Be A Valid Id From The Answers List";
        public const string QuestionsCollectionIsEmpty = "The Exam's Questions Is Not Initialized (Developer Side Issue)";
        public const string UnexpectedError = "An Unexpected Error Occurred. Please Try Again Later.";
        public const string ExamNotFound = "The Exam You Are Looking For Does Not Exist";
        public const string ExamAlreadyExists = "An Exam With This Name Already Exists";
        public const string InvalidExamTime = "The Exam Time Is Invalid, It Should Be Greater Than 0";
        public const string InvalidExamType = "The Exam Type Is Invalid, It Should Be 1 For Final Exam Or 2 For Practical Exam";
        public const string InvalidAnswerText = "The Answer Text Is Invalid, It Should Not Be Null Or Empty";

    }
}
