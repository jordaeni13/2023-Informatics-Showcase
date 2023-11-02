using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobHandler
{
    int doneJobs;
    int end = 1;
    string id = "";
    public JobHandler(int length, string id)
    {
        doneJobs = 0;
        this.end = toBit(length);
        this.id = id;
    }
    
    public int toBit(object job)
    {
        return (int) Mathf.Pow(2, (int) job);
    }
    public bool isDone(object job)
    {
        return (doneJobs & toBit(job)) == toBit(job);
    }

    public bool isPost(object job)
    {
        return toBit(job) - 1 == doneJobs;
    }
    public bool isDone(object job1, object job2)
    {
        return isDone(job1) && isDone(job2);
    }

    public void setDone(object job)
    {
        doneJobs |= toBit(job);
        Debug.Log("DoneJobs : " + id + doneJobs);
    }
    public bool allDone()
    {
        if (end - 1 == doneJobs)
        {
            Debug.Log(id + " : Job Done");
        }
        return end - 1 == doneJobs;
    }
}
