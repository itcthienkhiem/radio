#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Collections.Generic;
using System.Text;

using ClearCanvas.Dicom;
using ClearCanvas.Dicom.IO;

namespace ClearCanvas.Dicom.DicomDump
{
    class DicomDump
    {

        static DicomDumpOptions _options = DicomDumpOptions.Default;

        static void PrintCommandLine()
        {
            Console.WriteLine("DicomDump Parameters:");
            Console.WriteLine("\t-h\tDisplay this help information");
            Console.WriteLine("\t-g\tInclude group lengths");
            Console.WriteLine("\t-c\tAllow more than 80 characters per line");
            Console.WriteLine("\t-l\tDisplay long values");
            Console.WriteLine("All other parameters are considered filenames to list.");
        }

        static bool ParseArgs(string[] args)
        {
            foreach (String arg in args)
            {
                if (arg.ToLower().Equals("-h"))
                {
                    PrintCommandLine();
                    return false;
                }
                else if (arg.ToLower().Equals("-g"))
                {
                    _options &= ~DicomDumpOptions.KeepGroupLengthElements;
                }
                else if (arg.ToLower().Equals("-c"))
                {
                    _options &= ~DicomDumpOptions.Restrict80CharactersPerLine;
                }
                else if (arg.ToLower().Equals("-l"))
                {
                    _options &= ~DicomDumpOptions.ShortenLongValues;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintCommandLine();
                return;
            }

            if (false == ParseArgs(args))
                return;

            foreach (String filename in args)
            {
                if (filename.StartsWith("-"))
                    continue;

                DicomFile file = new DicomFile(filename);

                DicomReadOptions readOptions = DicomReadOptions.Default;

				try
				{
					file.Load(readOptions);
				}
				catch (Exception e)
				{
					Console.WriteLine("Unexpected exception when loading file: {0}", e.Message);
				}

                StringBuilder sb = new StringBuilder();

                file.Dump(sb, "", _options);

                Console.WriteLine(sb.ToString());
            }
        }
    }
}
