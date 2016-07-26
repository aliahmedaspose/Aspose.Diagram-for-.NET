﻿using Aspose.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Aspose.Diagram.Examples.CSharp.Load_Save_Convert
{
    public class ReadVisioDiagram
    {
        public static void Run()
        {
            // ExStart:ReadVisioDiagram
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir_LoadSaveConvert();

            // call the diagram constructor to load a VSD stream
            FileStream st = new FileStream(dataDir + "Drawing1.vsdx", FileMode.Open);
            Diagram vsdDiagram = new Diagram(st);
            st.Close();

            // Call the diagram constructor to load a VDX diagram
            Diagram vdxDiagram = new Diagram(dataDir + "Drawing1.vdx");

            /*
             * Call diagram constructor to load a VSS stencil
             * providing load file format
            */
            Diagram vssDiagram = new Diagram(dataDir + "Basic.vss", LoadFileFormat.VSS);

            /*
             * Call diagram constructor to load diagram from a VSX file
             * providing load options
            */
            LoadOptions loadOptions = new LoadOptions(LoadFileFormat.VSX);
            Diagram vsxDiagram = new Diagram(dataDir + "Drawing1.vsx", loadOptions);
            // ExEnd:ReadVisioDiagram
        }
    }
}
