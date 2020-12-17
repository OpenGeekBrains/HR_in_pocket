namespace HRInPocket.Domain
{
    public enum Sex
    {
        Male,Female
    }


    public enum AssignmentType
    {
        Invitation, Resume, Covering, Feedback
    }

    public enum TaskState
    {
        /// <summary>
        /// Новый заказ который сформирован, но еще не оплачен
        /// </summary>
        New,

        /// <summary>
        /// Заказ оплачен, и принят в работу (назначен менеджер и т.д.)
        /// </summary>
        Accepted,

        /// <summary>
        /// Заказ выполнен
        /// </summary>
        Completed
    }
}