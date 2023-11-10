using System.Text;
using Renci.SshNet;
using ConnectionInfo = Renci.SshNet.ConnectionInfo;

namespace vm_manager.Services;

public class JobService
{
    private readonly Dictionary<int, Job> _jobs = new();
    private static int nextJobCounter = 0;
    
    public int ScheduleJob(ConnectionInfo connectionInfo, List<string> commands)
    {
        var jobId = nextJobCounter;
        nextJobCounter++;
        //Create SSH connection
        var client = new SshClient(connectionInfo);
        client.Connect();
        
        //Create job
        var job = new Job(client, commands, new StringBuilder());
        
        //Add job to dictionary
        _jobs.Add(jobId, job);
        
        job.Start();
        


        return jobId;
    }

    public string GetJobOutput(int id)
    {
        var job = _jobs.GetValueOrDefault(id);
        return job == null ? "Job not found" : job.Output.ToString();
    }
}

public class Job
{
    public SshClient Client;
    public List<string> Commands;
    public StringBuilder Output;
    public Task? Task;

    public Job(SshClient client, List<string> commands, StringBuilder output)
    {
        this.Client = client;
        Commands = commands;
        Output = output;
    }

    public void Start()
    {
        //Create new Task on the threadpool to execute the job
        
        Task = Task.Run(async () =>
        {
            foreach (var command in Commands)
            {
                var commandObj = Client.CreateCommand(command);
                var asyncExecute = commandObj.BeginExecute();
                //Add stream reader to get output
                using var reader = new StreamReader(commandObj.OutputStream);
                
                while (!asyncExecute.IsCompleted)
                {
                    Output.Append(await reader.ReadToEndAsync());
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                }
                
                Output.Append(await reader.ReadToEndAsync());
            }
            
            Client.Disconnect();
        });
    }
}