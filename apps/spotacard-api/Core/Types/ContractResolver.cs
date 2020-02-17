using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
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
    public IContract Get(ContractTypes type, Card card)
    {
      switch (type)
      {
        case ContractTypes.Todo:
          return new TodoContract(card);
        case ContractTypes.Project:
          return new ProjectContract(card);
        case ContractTypes.Task:
          return new TaskContract(card);
        case ContractTypes.TaskList:
          return new TaskListContract(card);
        case ContractTypes.App:
          return new AppContract(card);
        case ContractTypes.Workflow:
          return new WorkflowContract(card);
        case ContractTypes.Step:
          return new StepContract(card);
        case ContractTypes.Template:
          return new TemplateContract(card);
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid contract type");
      }
    }
  }
}
