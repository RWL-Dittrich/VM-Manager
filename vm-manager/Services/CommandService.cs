using Renci.SshNet;

namespace vm_manager.Services;

public class CommandService
{
    public async Task<bool> ExecuteCommand(SshClient client, string command)
    {
        using (var commandObj = client.CreateCommand(command))
        {
            var asyncExecute = commandObj.BeginExecute();

            //Add stream reader to get output
            using var reader = new StreamReader(commandObj.OutputStream);
                
            while (!asyncExecute.IsCompleted)
            {
                Console.Write(await reader.ReadToEndAsync());
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }
            
            Console.Write(await reader.ReadToEndAsync());

            return commandObj.ExitStatus == 0;
        }
        
    }
}