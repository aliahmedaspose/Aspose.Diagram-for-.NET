﻿using Aspose.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aspose.Diagram.Examples.CSharp.Working_with_SolutionXML_Elements
{
    public class ReadSolutionXMLElement
    {
        public static void Run() 
        {
            // ExStart:ReadSolutionXMLElement
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir_SolutionXML();

            // Load source Visio diagram
            Diagram diagram = new Diagram(dataDir + "Drawing1.vsdx");

            // Iterate through SolutionXML elements
            foreach (SolutionXML solutionXML in diagram.SolutionXMLs)
            {
                // Get name property
                Console.WriteLine(solutionXML.Name);
                // Get xml value
                Console.WriteLine(solutionXML.XmlValue);
            }
            // ExEnd:ReadSolutionXMLElement
        }
    }
}
