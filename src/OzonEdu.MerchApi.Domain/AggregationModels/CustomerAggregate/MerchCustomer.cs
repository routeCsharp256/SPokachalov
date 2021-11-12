using System.Text.RegularExpressions;
using OzonEdu.MerchApi.Domain.Exceptions.CustomerAggregate;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate
{
    public sealed class MerchCustomer:Entity
    {
        /// <summary>
        /// Почта содтрудника - заявителя на получение набора мерча
        /// </summary>
        public MailCustomer Mail { get; private set; }
        /// <summary>
        /// ФИО содтрудника - заявителя на получение набора мерча
        /// </summary>
        public  NameCustomer Name { get; }
        /// <summary>
        /// Почта "наставника" содтрудника - заявителя на получение набора мерча (например HR)
        /// </summary>
        public MailCustomer MentorMail { get; private set; }
        
        /// <summary>
        /// ФИО "наставника" содтрудника - заявителя на получение набора мерча (например HR)
        /// </summary>
        public NameCustomer MentorName { get; private set; }

        public MerchCustomer(MailCustomer mail, NameCustomer name)
        {
            Name = name;
            SetMail(mail);
        }

        private void SetMail(MailCustomer mail)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(mail.Value);
            if (!match.Success)
                throw new NotValidMailException("Не верный формат почты!");
            Mail = mail;
        }
        
        public void SetMentorMail(MailCustomer mail)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(mail.Value);
            if (!match.Success)
                throw new NotValidMailException("Не верный формат почты!");
            MentorMail = mail;
        }
        
        public void SetMentorName(NameCustomer name)
        {
            MentorName = name;
        }
    }
}