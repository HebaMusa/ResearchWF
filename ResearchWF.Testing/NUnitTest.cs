using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ResearchWF.Model;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Testing;

namespace ResearchWF.Testing
{
	public class NUnitTest : WorkflowTest<ResearchManagerWF, WFState>
	{
		[SetUp]
		protected override void Setup()
		{
			base.Setup();
		}

		[Test]
		public void NUnit_workflow_test_sample()
		{
			//var workflowId = StartWorkflow(new WFState() { Value1 = 2, Value2 = 3 });
			//WaitForWorkflowToComplete(workflowId, TimeSpan.FromSeconds(30));

			//GetStatus(workflowId).Should().Be(WorkflowStatus.Complete);
			//UnhandledStepErrors.Count.Should().Be(0);
			//GetData(workflowId).Value3.Should().Be(5);
		}

	}
}
