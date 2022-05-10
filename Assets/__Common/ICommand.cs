namespace __Common
{
    public interface ICommand
    {
        SuccessFactor SuccessFactor { get; }
        
        /// <summary>
        /// The method that invokes the logic in the receiver.
        /// </summary>
        void Execute();

        /// <summary>
        /// The method that invokes the reverse behaviour of the execute in the receiver.
        /// </summary>
        void Unexecute();
    }
}
