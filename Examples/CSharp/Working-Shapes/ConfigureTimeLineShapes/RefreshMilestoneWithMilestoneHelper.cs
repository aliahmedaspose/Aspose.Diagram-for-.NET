using System.IO;
using Aspose.Diagram;
using System;

namespace Aspose.Diagram.Examples.CSharp.Shapes
{
    public class RefreshMilestoneWithMilestoneHelper
    {
        public static void Run()
        {
            // ExStart:RefreshMilestoneWithMilestoneHelper
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir_Shapes();

            string pageName = "Page-1";

            ////////////// Modify time line /////////// 
            DateTime startDate = new DateTime(2015, 8, 1);
            DateTime endDate = new DateTime(2016, 6, 1);
            DateTime fisYear = startDate;

            // Load a diagram 
            Diagram diagram = new Diagram(dataDir + "DrawingTimeLine.vsdx");

            // Get page
            Aspose.Diagram.Page page = diagram.Pages.GetPage(pageName);

            long timelineId = 1;
            Shape timeline = diagram.Pages.GetPage(pageName).Shapes.GetShape(timelineId);
            double xpos = timeline.XForm.PinX.Value;
            double ypos = timeline.XForm.PinY.Value;

            // Add milestone 
            string milestoneMasterName = "2 triangle milestone";

            // Add Master
            diagram.AddMaster(dataDir + "Timeline.vss", milestoneMasterName);

            // Add Shape in Visio diagram using AddShape method
            long milestoneShapeId = diagram.AddShape(xpos, ypos, milestoneMasterName, 0);

            // Get the shape based on ID
            Shape milestone = page.Shapes.GetShape(milestoneShapeId);

            // Instantiate MilestoneHelper object
            MilestoneHelper milestoneHelper = new MilestoneHelper(milestone);

            // Set Milestone Date
            milestoneHelper.MilestoneDate = new DateTime(2015, 8, 1);

            // Set IsAutoUpdate to true
            milestoneHelper.IsAutoUpdate = true;

            // RefreshMilesone of timeline shape
            milestoneHelper.RefreshMilestone(timeline);

            // Save Visio file
            diagram.Save(dataDir + "RefreshMilestone_out.vsdx", SaveFileFormat.VSDX);
            // ExEnd:RefreshMilestoneWithMilestoneHelper
        }
    }
}