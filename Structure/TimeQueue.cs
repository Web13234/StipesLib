using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StipesLib.Structure;

public class TimeQueue<T>
{
    LinkedList<(T item, ulong targetTime)> List = new();

    private ulong Time = 0;
    public void TimeLapes(uint delta) => Time += delta;

    /// <summary>
    /// try get ready item in queue,out item and overflowed time,if failed,return false
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    public bool TryPull(out (T item, int overFlowedTime) cell)
    {
        if(List.First != null)
        {
            var (item, targetTime) = List.First.ValueRef;
            int overFlowedTime = (int)(Time - targetTime);
            if (overFlowedTime >= 0)
            {
                cell = (item, overFlowedTime);
                List.RemoveFirst();
                return true;
            }
        }
        cell = new();
        return false;
    }
    /// <summary>
    /// get ready item in queue,return item and overflowed time
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public (T item,int overFlowedTime) Pull()
    {
        if(TryPull(out var cell))
        {
            return cell;
        }
        else
        {
            throw new Exception();
        }
    }

    public void Insert(T item, int time) => Insert((item, time));
    public void Insert((T item,int time) cell)
    {
        ulong targetTime = Time + (uint)cell.time;
        var listNode = List.First;
        while (listNode != null)
        {
            if (listNode.Value.targetTime >= targetTime)
            {
                List.AddBefore(listNode, (cell.item, targetTime));
                return;
            }
            listNode = listNode?.Next;
        }
        List.AddLast((cell.item, targetTime));
    }
}