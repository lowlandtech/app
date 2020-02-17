using NUnit.Framework;
using Spotacard.Core.Enums;
using Spotacard.Core.Types;
using Spotacard.Features.Apps;
using Spotacard.Features.Projects;
using Spotacard.Features.Steps;
using Spotacard.Features.TaskLists;
using Spotacard.Features.Tasks;
using Spotacard.Features.Templates;
using Spotacard.Features.Todos;
using Spotacard.Features.Workflows;

namespace Spotacard.Features.Contracts
{
  public class ContractTests
  {
    [Test]
    public void ShouldResolveTemplate()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Template);
      Assert.That(contract, Is.InstanceOf(typeof(TemplateContract)));
    }

    [Test]
    public void ShouldResolveTodo()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Todo);
      Assert.That(contract, Is.InstanceOf(typeof(TodoContract)));
    }

    [Test]
    public void ShouldResolveTask()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Task);
      Assert.That(contract, Is.InstanceOf(typeof(TaskContract)));
    }

    [Test]
    public void ShouldResolveTaskList()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.TaskList);
      Assert.That(contract, Is.InstanceOf(typeof(TaskListContract)));
    }

    [Test]
    public void ShouldResolveApp()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.App);
      Assert.That(contract, Is.InstanceOf(typeof(AppContract)));
    }

    [Test]
    public void ShouldResolveWorkflow()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Workflow);
      Assert.That(contract, Is.InstanceOf(typeof(WorkflowContract)));
    }

    [Test]
    public void ShouldResolveStep()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Step);
      Assert.That(contract, Is.InstanceOf(typeof(StepContract)));
    }

    [Test]
    public void ShouldResolveProject()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Project);
      Assert.That(contract, Is.InstanceOf(typeof(ProjectContract)));
    }

  }
}
