using NUnit.Framework;
using Spotacard.Core.Enums;
using Spotacard.Core.Types;
using Spotacard.Domain;
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
    private readonly CardFixtures _fixture;

    public ContractTests()
    {
      _fixture = new CardFixtures();
    }

    [Test]
    public void ShouldResolveTemplate()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Template, _fixture.Template);
      Assert.That(contract, Is.InstanceOf(typeof(TemplateContract)));
      Assert.That(contract.Card, Is.Not.Null);
    }

    [Test]
    public void ShouldResolveTodo()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Todo, _fixture.Todo);
      Assert.That(contract, Is.InstanceOf(typeof(TodoContract)));
      Assert.That(contract.Card, Is.Not.Null);
    }

    [Test]
    public void ShouldResolveTask()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Task, _fixture.Task);
      Assert.That(contract, Is.InstanceOf(typeof(TaskContract)));
      Assert.That(contract.Card, Is.Not.Null);
    }

    [Test]
    public void ShouldResolveTaskList()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.TaskList, _fixture.TaskList);
      Assert.That(contract, Is.InstanceOf(typeof(TaskListContract)));
      Assert.That(contract.Card, Is.Not.Null);
    }

    [Test]
    public void ShouldResolveApp()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.App, _fixture.App);
      Assert.That(contract, Is.InstanceOf(typeof(AppContract)));
      Assert.That(contract.Card, Is.Not.Null);
    }

    [Test]
    public void ShouldResolveWorkflow()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Workflow, _fixture.Workflow);
      Assert.That(contract, Is.InstanceOf(typeof(WorkflowContract)));
      Assert.That(contract.Card, Is.Not.Null);
    }

    [Test]
    public void ShouldResolveStep()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Step, _fixture.Step);
      Assert.That(contract, Is.InstanceOf(typeof(StepContract)));
      Assert.That(contract.Card, Is.Not.Null);
    }

    [Test]
    public void ShouldResolveProject()
    {
      var resolver = new ContractResolver();
      var contract = resolver.Get(ContractTypes.Project, _fixture.Project);
      Assert.That(contract, Is.InstanceOf(typeof(ProjectContract)));
      Assert.That(contract.Card, Is.Not.Null);
    }
  }

  public class CardFixtures
  {
    public Card Template { get; set; }
    public Card Todo { get; set; }
    public Card Task { get; set; }
    public Card Workflow { get; set; }
    public Card Step { get; set; }
    public Card Project { get; set; }
    public Card TaskList { get; set; }
    public Card App { get; set; }
  }
}
