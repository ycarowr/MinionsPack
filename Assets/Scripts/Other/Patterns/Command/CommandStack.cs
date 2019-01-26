using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();

    void Unexecute();
}

public class CommandStack<T> : SingletonMB<T> where T : MonoBehaviour
{
    private Stack<ICommand> Commands = new Stack<ICommand>();
    public bool IsEmpty { get { return Commands.Count == 0; } }

    public virtual void Push(ICommand command)
    {
        Debug.Log("Push: "+command);
        Commands.Push(command);
    }

    public virtual ICommand Pop()
    {
        if (!IsEmpty)
        {
            ICommand command = Commands.Pop();
            command.Execute();
            Debug.Log("Pop: " + command);
            if (Commands.Count == 0)
                OnEmptyStack();
            return command;
        }
        else
            return null;
    }

    public virtual void OnEmptyStack()
    {

    }
}
