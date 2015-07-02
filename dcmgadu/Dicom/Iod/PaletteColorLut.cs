#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Drawing;
using ClearCanvas.Common;

namespace ClearCanvas.Dicom.Iod
{
	public partial class PaletteColorLut
	{
		private readonly string _sourceSopInstanceUid;
		private readonly int _firstMappedPixelValue;
		private readonly int _countEntries;
		private readonly Color[] _data;

		//TODO (CR Sept 2010): maybe make the source sop instance UID a settable property, since it's not mandatory.
		public PaletteColorLut(string sourceSopInstanceUid,
		                       int size,
		                       int firstMappedPixel,
		                       int bitsPerLutEntry,
		                       byte[] redLut,
		                       byte[] greenLut,
		                       byte[] blueLut)

		{
			Platform.CheckForNullReference(redLut, "redLut");
			Platform.CheckForNullReference(greenLut, "greenLut");
			Platform.CheckForNullReference(blueLut, "blueLut");
			Platform.CheckTrue(redLut.Length == greenLut.Length, "redLut.Length == greenLut.Length");
			Platform.CheckTrue(redLut.Length == blueLut.Length, "redLut.Length == blueLut.Length");
			Platform.CheckTrue(redLut.Length == size || (redLut.Length == 2 * size && bitsPerLutEntry > 8), "Valid Lut Size");

			_sourceSopInstanceUid = sourceSopInstanceUid;
			_countEntries = size;
			_firstMappedPixelValue = firstMappedPixel;
			_data = Create(size, bitsPerLutEntry, redLut, greenLut, blueLut);
		}

		public string SourceSopInstanceUid
		{
			get { return _sourceSopInstanceUid; }
		}

		public int FirstMappedPixelValue
		{
			get { return _firstMappedPixelValue; }
		}

		//TODO (CR Sept 2010): name?
		public int CountEntries
		{
			get { return _countEntries; }
		}
		
		public Color[] Data
		{
			get { return _data; }	
		}

		public Color this[int index]
		{
			get
			{
				if (index < _firstMappedPixelValue)
					return _data[0];

				if (index > _firstMappedPixelValue + _data.Length - 1)
					return _data[_data.Length - 1];

				return _data[index - _firstMappedPixelValue];
			}
		}

		private Color[] Create(int size, int bitsPerLutEntry, byte[] redLut, byte[] greenLut, byte[] blueLut)
		{
			Color[] lut = new Color[size];

			if (bitsPerLutEntry == 8)
			{
				// Account for case where an 8-bit entry is encoded in a 16 bits allocated
				// i.e., 8 bits of padding per entry
				if (redLut.Length == 2 * size)
				{
					int offset = 0;
					for (int i = 0; i < size; i++)
					{
						// Get the low byte of the 16-bit entry
						lut [i] = Color.FromArgb(255, redLut[offset], greenLut[offset], blueLut[offset]);
						offset += 2;
					}
				}
				else
				{
					// The regular 8-bit case
					int offset = 0;
					for (int i = 0; i < size; i++)
					{
						lut[i] = Color.FromArgb(255, redLut[offset], greenLut[offset], blueLut[offset]);
						++offset;
					}
				}
			}
			// 16 bit entries
			else
			{
				int offset = 1;
				for (int i = 0; i < size; i++)
				{
					// Just get the high byte, since we'd have to right shift the
					// 16-bit value by 8 bits to scale it to an 8 bit value anyway.
					lut[i] = Color.FromArgb(255, redLut[offset], greenLut[offset], blueLut[offset]);
					offset += 2;
				}
			}

			return lut;
		}

		public static PaletteColorLut Create(IDicomAttributeProvider dataSource)
		{
			int lutSize, firstMappedPixel, bitsPerLutEntry;
			string sourceSopInstanceUid;

			DicomAttribute attribDescriptor = dataSource[DicomTags.RedPaletteColorLookupTableDescriptor];

			bool tagExists = attribDescriptor.TryGetInt32(0, out lutSize);
			if (!tagExists)
				throw new Exception("LUT Size missing.");

			tagExists = attribDescriptor.TryGetInt32(1, out firstMappedPixel);

			if (!tagExists)
				throw new Exception("First Mapped Pixel missing.");

			tagExists = attribDescriptor.TryGetInt32(2, out bitsPerLutEntry);

			if (!tagExists)
				throw new Exception("Bits Per Entry missing.");

			byte[] redLut = dataSource[DicomTags.RedPaletteColorLookupTableData].Values as byte[];
			if (redLut == null)
				throw new Exception("Red Palette Color LUT missing.");

			byte[] greenLut = dataSource[DicomTags.GreenPaletteColorLookupTableData].Values as byte[];
			if (greenLut == null)
				throw new Exception("Green Palette Color LUT missing.");

			byte[] blueLut = dataSource[DicomTags.BluePaletteColorLookupTableData].Values as byte[];
			if (blueLut == null)
				throw new Exception("Blue Palette Color LUT missing.");

			// The DICOM standard says that if the LUT size is 0, it means that it's 65536 in size.
			if (lutSize == 0)
				lutSize = 65536;

			//TODO (CR Sept 2010): just leave it blank?  There are plenty of use cases for this class without need of a source sop instance uid
			if (!dataSource[DicomTags.SopInstanceUid].TryGetString(0, out sourceSopInstanceUid))
				sourceSopInstanceUid = DicomUid.GenerateUid().UID;

			return new PaletteColorLut(
				sourceSopInstanceUid,
				lutSize,
				firstMappedPixel,
				bitsPerLutEntry,
				redLut,
				greenLut,
				blueLut);

		}

	}
}