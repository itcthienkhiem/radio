#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Windows.Forms;

namespace ClearCanvas.Dicom.Samples
{
	public partial class DicomdirDisplay : Form
	{
		public DicomdirDisplay()
		{
			InitializeComponent();
		}

		private void AddTagNodes(TreeNode parent, DicomSequenceItem record)
		{
			foreach (DicomAttribute attrib in record)
			{
				string name;
				if (attrib is DicomAttributeSQ || attrib is DicomAttributeOB || attrib is DicomAttributeOW || attrib is DicomAttributeOF)
					name = attrib.ToString();
				else
				{
					name = String.Format("{0}: {1}", attrib.Tag.ToString(), attrib.ToString());
				}
				TreeNode tagNode = new TreeNode(name);
				parent.Nodes.Add(tagNode);

				DicomAttributeSQ sqAttrib = attrib as DicomAttributeSQ;
				if (sqAttrib != null)
				{
					for (int i=0; i< sqAttrib.Count; i++)
					{
						TreeNode sqNode = new TreeNode("Sequence Item");
						tagNode.Nodes.Add(sqNode);
						AddTagNodes(sqNode, sqAttrib[i]);
					}
				}
			}
		}

		public void Add(DicomDirectory dir)
		{
			_treeViewDicomdir.BeginUpdate();
			_treeViewDicomdir.TopNode = new TreeNode();
			
			TreeNode topNode = new TreeNode("DICOMDIR: " + dir.FileSetId);

			_treeViewDicomdir.Nodes.Add( topNode);

			foreach (DirectoryRecordSequenceItem patientRecord in dir.RootDirectoryRecordCollection)
			{
				TreeNode patientNode = new TreeNode(patientRecord.ToString());
				topNode.Nodes.Add(patientNode);

				AddTagNodes(patientNode, patientRecord);

				foreach (DirectoryRecordSequenceItem studyRecord in patientRecord.LowerLevelDirectoryRecordCollection)
				{
					TreeNode studyNode = new TreeNode(studyRecord.ToString());
					patientNode.Nodes.Add(studyNode);

					AddTagNodes(studyNode, studyRecord);

					foreach (DirectoryRecordSequenceItem seriesRecord in studyRecord.LowerLevelDirectoryRecordCollection)
					{
						TreeNode seriesNode = new TreeNode(seriesRecord.ToString());
						studyNode.Nodes.Add(seriesNode);

						AddTagNodes(seriesNode, seriesRecord);

						foreach (DirectoryRecordSequenceItem instanceRecord in seriesRecord.LowerLevelDirectoryRecordCollection)
						{
							TreeNode instanceNode = new TreeNode(instanceRecord.ToString());
							seriesNode.Nodes.Add(instanceNode);

							AddTagNodes(instanceNode, instanceRecord);
						}
					}
				}
			}
			_treeViewDicomdir.EndUpdate();
		}
	}
}
