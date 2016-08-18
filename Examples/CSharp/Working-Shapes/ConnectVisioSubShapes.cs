﻿using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aspose.Diagram.Examples.CSharp.Working_Shapes
{
    public class ConnectVisioSubShapes
    {
        public static void Run()
        {
            // ExStart:ConnectVisioSubShapes
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir_Shapes();

            // Set sub shape ids
            long shapeFromId = 2;
            long shapeToId = 4;

            // Load diagram
            Diagram diagram = new Diagram(dataDir + "Drawing1.vsdx");
            // Access a particular page
            Page page = diagram.Pages.GetPage("Page-3");
           
            // Initialize connector shape
            Shape shape = new Shape();
            shape.Line.EndArrow.Value = 4;
            shape.Line.LineWeight.Value = 0.01388;

            // Add shape
            long connecter1Id = diagram.AddShape(shape, "Dynamic connector", page.ID);
            // Connect sub-shapes
            page.ConnectShapesViaConnector(shapeFromId, ConnectionPointPlace.Right, shapeToId, ConnectionPointPlace.Left, connecter1Id);
            // Save Visio drawing
            diagram.Save(dataDir + "ConnectVisioSubShapes_Out.vsdx", SaveFileFormat.VSDX);
            // ExEnd:ConnectVisioSubShapes
        }
    }
}
