namespace Syko.UnityToolbox.Commands
{
    public class CommandSystem
    {
        protected ICommand[] history;
        protected int undoIndex = -1;
        protected int latestCommandIndex = -1;
        protected bool hasLatestCommandBeenExecuted = false;
        public bool CanUndo
        {
            get
            {
                return (undoIndex != latestCommandIndex
                || hasLatestCommandBeenExecuted)
                && history[undoIndex] != null;
            }
        }
        public bool CanRedo
        {
            get
            {
                return (undoIndex != latestCommandIndex
                || !hasLatestCommandBeenExecuted)
                && history[(undoIndex +1) % history.Length] != null;
            }
        }

        public CommandSystem(int historySize)
        {
            history = new ICommand[historySize];
        }

        protected CommandSystem(ICommand[] history, int undoIndex, int latestCommandIndex, bool hasLatestCommandBeenExecuted)
        {
            this.history = history;
            this.undoIndex = undoIndex;
            this.latestCommandIndex = latestCommandIndex;
            this.hasLatestCommandBeenExecuted = hasLatestCommandBeenExecuted;
        }

        public void Execute (ICommand command)
        {
            undoIndex = (undoIndex + 1) % history.Length;
            latestCommandIndex = undoIndex;
            history[undoIndex] = command;
            command.Execute();
            hasLatestCommandBeenExecuted = true;
        }

        public void Undo ()
        {
            if (!CanUndo) return;
            history[undoIndex].Undo();
            undoIndex = (undoIndex + history.Length - 1) % history.Length;
            hasLatestCommandBeenExecuted = false;
        }

        public void Redo()
        {
            if (!CanRedo) return;
            undoIndex = (undoIndex + 1) % history.Length;
            history[undoIndex].Execute();
            if (undoIndex == latestCommandIndex) hasLatestCommandBeenExecuted = true;
        }

        public void Clear ()
        {
            undoIndex = -1;
            latestCommandIndex = -1;
            hasLatestCommandBeenExecuted = false;
        }

        public CommandSystem Clone()
        {
            return new CommandSystem((ICommand[])history.Clone(), undoIndex, latestCommandIndex, hasLatestCommandBeenExecuted);
        }
    }

    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}
