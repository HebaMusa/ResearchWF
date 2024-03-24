using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResearchWF;
using ResearchWF.Enums;
using ResearchWF.Middleware;
using ResearchWF.Model;
using ResearchWF.Services;
using ResearchWF.Services.Interfaces;
using ResearchWF.Steps;
using System.Xml.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services.DefinitionStorage;

namespace TestWF
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IServiceProvider serviceProvider = ConfigureServices();

			//start the workflow host
			var host = serviceProvider.GetService<IWorkflowHost>();
			var controller = serviceProvider.GetService<IWorkflowController>();
			//var loader = serviceProvider.GetService<IDefinitionLoader>();
			//loader.LoadDefinition(Properties.Resources.ContentWF, Deserializers.Json);

			host.RegisterWorkflow<ResearchManagerWF, WFState>();
			host.Start();

			Console.Write("Enter next state number:");
			int value = Convert.ToInt16(Console.ReadLine());


			var arbitratorsIdsForThisResearch = new List<int> {10,122,14,16 };
			var eventKey = Guid.NewGuid().ToString();
			var initialData = new WFState() { 
				ContentCurrentState = value , 
				userRole= (int)RoleName.ArbitratorSupervisor, 
				outputDecision = (int)UserDecision.MoveForward, 
				getArbitratorsEventId = eventKey,
				Description ="---  custom data to be display in log ----"
			};
			//var workflowId = controller.StartWorkflow<WFState>("ResearchManagerWF", initialData).Result;
			//// workflowId = host.StartWorkflow("ResearchManagerWF", 1, initialData).Result;

			////var userId = "10";
			////host.PublishEvent("GetUserData", workflowId, userId);	
			

			controller.StartWorkflow<WFState>("ResearchManagerWF", initialData);

			host.PublishEvent("GetResearchArbitrators", eventKey, arbitratorsIdsForThisResearch);

			Console.ReadLine();
			host.Stop();
		}

		private static IServiceProvider ConfigureServices()
		{
			//setup dependency injection
			IServiceCollection services = new ServiceCollection();

			services.AddLogging(cfg =>
			{
				cfg.AddConsole(x => x.IncludeScopes = true);
				cfg.AddDebug();
			});

			//services.AddLogging();
			services.AddWorkflow(x => x.UseSqlServer(@"Server=.;Database=DBResearchesWF;User Id=WFUser;Password=P@ssw0rd;TrustServerCertificate=true;", true, true));


			// Add step middleware
			// Note that middleware will get executed in the order in which they were registered
			services.AddWorkflowStepMiddleware<AddMetadataToLogsMiddleware>();

			// Add some pre workflow middleware
			// This middleware will run before the workflow starts
			services.AddWorkflowMiddleware<AddDescriptionWorkflowMiddleware>();

			// Add some post workflow middleware
			// This middleware will run after the workflow completes
			services.AddWorkflowMiddleware<PrintWorkflowSummaryMiddleware>();

			services.AddTransient<LogMessage>();
			services.AddTransient<AcceptedFromArbitrator>();
			services.AddTransient<AcceptedFromArbitratorSupervisor>();
			services.AddTransient<AcceptedFromEditorManager>();
			services.AddTransient<CompletedFromEditorManager>();
			services.AddTransient<NewStep>();
			services.AddTransient<PendingReviewFromArbitrator>();
			services.AddTransient<PendingReviewFromArbitratorSupervisor>();
			services.AddTransient<PendingReviewFromEditorManager>();
			services.AddTransient<RejectedFromArbitrator>();
			services.AddTransient<RejectedFromArbitratorSupervisor>();
			services.AddTransient<RejectedFromEditorManager>();
			services.AddTransient<ReturnedToSourceForUpdates>();
			services.AddTransient<UnderReviewFromArbitrator>();
			services.AddTransient<UnderReviewFromEditorManager>();

			services.AddTransient<IManagingEditorService, ManagingEditorService>();
			services.AddTransient<IResearchService, ResearchService>();

			var serviceProvider = services.BuildServiceProvider();

			return serviceProvider;
		}

	}
}
