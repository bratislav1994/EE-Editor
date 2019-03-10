using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class CommandInvoker
    {
        private List<Command> history = new List<Command>();
        private List<Command> redo = new List<Command>();

        public CommandInvoker()
        {

        }

        public void AddAndExecute(Command command)
        {
            history.Add(command);
            command.Execute();
            redo.Clear();
        }

        public void Undo()
        {
            if (history.Count != 0)
            {
                Command com = history[history.Count - 1];
                com.UnExecute();
                history.Remove(com);
                redo.Add(com);
            }
        }

        public void Redo()
        {
            if (redo.Count != 0)
            {
                Command command = redo[redo.Count - 1];
                redo.Remove(command);
                command.Execute();
                history.Add(command);
            }
        }
    }
}
