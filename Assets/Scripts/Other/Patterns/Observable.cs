using System;

public class Observable<T>
{
    private T MyInternalValue;
    public EventHandler<ChangedEventArgs> Changed;

    public T Value
    {
        get
        {
            return MyInternalValue;
        }
        set
        {
            if (!value.Equals(MyInternalValue))
            {
                T oldValue = MyInternalValue;
                MyInternalValue = value;
                EventHandler<ChangedEventArgs> handler = Changed;
                if (handler != null)
                {
                    handler(this, new ChangedEventArgs { OldValue = oldValue, NewValue = value });
                }
            }
        }
    }

    public class ChangedEventArgs : EventArgs
    {
        public T OldValue;
        public T NewValue;
    }
}