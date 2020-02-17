using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Features.Apps;
using Spotacard.Features.Projects;
using Spotacard.Features.Steps;
using Spotacard.Features.TaskLists;
using Spotacard.Features.Tasks;
using Spotacard.Features.Templates;
using Spotacard.Features.Todos;
using Spotacard.Features.Workflows;
using System;

namespace Spotacard.Core.Types
{
  public class ContractResolver
  {
    public IContract Get(ContractTypes type)
    {
      switch (type)
      {
        case ContractTypes.Todo:
          return new TodoContract();
        case ContractTypes.Project:
          return new ProjectContract();
        case ContractTypes.Task:
          return new TaskContract();
        case ContractTypes.TaskList:
          return new TaskListContract();
        case ContractTypes.App:
          return new AppContract();
        case ContractTypes.Workflow:
          return new WorkflowContract();
        case ContractTypes.Step:
          return new StepContract();
        case ContractTypes.Template:
          return new TemplateContract();
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid contract type");
      }
    }
  }
}
