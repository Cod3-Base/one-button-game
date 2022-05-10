using System.Collections.Generic;

namespace __Common
{
    public interface IMacroCommand : ICommand
    {
        new SuccessFactor SuccessFactor { get; set; }
        
        List<SuccessFactor> OtherSuccessFactors { get; }
        List<ICommand> CommandsToExecute { get; set; }
        List<ICommand> CommandHistory { get; }
    }
}
