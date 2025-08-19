using ExamSystem.Logic;
using ExamSystem.Models.Enums;
using ExamSystem.UI.Error_Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.Models.AbstractClasses
{
    public abstract class Question : ICloneable , IComparable<Question>
    {

        #region Automatic Properties
        protected int RightAnswerId { get; set; }
        public ICollection<Answer>? AnswersList { get; init; }
        public QuestionType Header { get; set; } 
        #endregion

        #region Full Properties
        private string? body;
        private int marks;
        private int chosenAnswerId;

        public string? Body
        {
            get => body;
            set 
            {
                if (Validators.IsValidBody(value))
                    body = value;
                else
                    throw new ArgumentException(Constants.InvalidQuestionBody);
            } 
        }
        public int Marks 
        { 
            get => marks;
            set 
            {
                if(Validators.IsValidMarks(value))
                    marks = value;
                else
                    throw new ArgumentException(Constants.InvalidQuestionMarks);
            }
        }
        public int ChosenAnswerId
        {
            get => chosenAnswerId;
            set 
            {
                if (Validators.IsValidChosenAnswerId(this,value)) 
                    chosenAnswerId = value;
                else
                    throw new ArgumentException(Constants.InvalidChosenAnswerId);
            }
        } 
        #endregion


        protected Question(QuestionType header, string? body, int marks)
        {
            Header = header;
            Body = body;
            Marks = marks;
        }
        public abstract void SetRightAnswerId(int Id);

        public Answer? GetAnswerById(int id)
        {
            if (Validators.IsAnswerExists(this))
            {
                if (Validators.IsValidId(this, id))
                    return AnswersList?.FirstOrDefault(a => a.Id == id);
            }
            return null;
        }

        public Answer GetRightAnswer()
        {
            if(Validators.IsAnswerExists(this))
            {
                if(Validators.IsValidId(this, RightAnswerId))
                    return AnswersList?.FirstOrDefault(a => a.Id == RightAnswerId) ?? new Answer(0, string.Empty);
            }
            throw new InvalidOperationException(Constants.EmptyAnswerList);
        }
        public Answer GetChosenAnswer()
        {
            if (Validators.IsAnswerExists(this))
            {
                if (Validators.IsValidId(this, ChosenAnswerId))
                    return AnswersList?.FirstOrDefault(a => a.Id == ChosenAnswerId) ?? new Answer(0 , string.Empty);
            }
            throw new InvalidOperationException(Constants.EmptyAnswerList);
        }
        public void SetChosenAnswerId(int id)
        {
            if (Validators.IsValidId(this, id))
            {
                ChosenAnswerId = id;
            }
            else
            {
                throw new InvalidOperationException(Constants.InvalidChosenAnswerId);
            }
        }
        #region Interfaces Implementaion
        public abstract object Clone();
        public int CompareTo(Question? other)
        {
            if (other == null) return 1;
            return Marks.CompareTo(other.Marks);
        } 
        public ICollection<Answer> CloneAnswersList()
        {
            if (AnswersList == null)
                throw new InvalidOperationException(Constants.EmptyAnswerList);
            return AnswersList.Select(answer => (Answer)answer.Clone()).ToList();
        }
        public string DisplayQuestion()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Body} ({Marks} points)");
            sb.AppendLine();
            if (AnswersList != null && AnswersList.Any())
            {
                foreach (var answer in AnswersList)
                {
                    sb.AppendLine($"{answer.Id}. {answer.AnswerText}");
                }
            }
            else
            {
                return Constants.EmptyAnswerList;
            }
            return sb.ToString();
        }
        #endregion

    }
}
