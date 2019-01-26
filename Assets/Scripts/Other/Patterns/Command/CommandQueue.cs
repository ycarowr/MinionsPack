using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandQueue<T> : SingletonMB<T> where T : MonoBehaviour
{
    protected Queue<ICommand> Commands = new Queue<ICommand>();

    public bool IsEmpty { get { return Commands.Count == 0; } }
    public int Size { get { return Commands.Count; } }

    public virtual void Enqueue(ICommand command)
    {
        Commands.Enqueue(command);
    }

    public virtual ICommand Dequeue()
    {
        if (!IsEmpty)
        {
            ICommand command = Commands.Dequeue();
            command.Execute();
            if (Commands.Count == 0)
                OnEmptyQueue();
            return command;
        }
        else
            return null;
    }

    public virtual void OnEmptyQueue()
    {

    }
}
