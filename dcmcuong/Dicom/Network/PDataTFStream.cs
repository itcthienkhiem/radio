#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.IO;

namespace ClearCanvas.Dicom.Network
{
    internal class PDataTFStream : Stream
    {
        public delegate void TickDelegate();

        #region Private Members
        private bool _command;
        private readonly uint _max;
        private readonly byte _pcid;
        private PDataTF _pdu;
        private byte[] _bytes;
        private MemoryStream _buffer;
        private readonly NetworkBase _networkBase;
    	private bool _combineCommandData;
        #endregion

        #region Public Constructors
        public PDataTFStream(NetworkBase networkBase, byte pcid, uint max, uint total, bool combineCommandData)
        {
            _command = true;
            _pcid = pcid;
            _max = max;
            _pdu = new PDataTF();
            _buffer = new MemoryStream((int)total + 1024);
            _networkBase = networkBase;
        	_combineCommandData = combineCommandData;
        }
        #endregion

        #region Public Properties
        public TickDelegate OnTick;

        public bool IsCommand
        {
            get { return _command; }
            set
            {
                CreatePDV(true);
                _command = value;
				if (!_combineCommandData)
					WritePDU(true);
            }
        }
        #endregion

        #region Public Members
        public void Flush(bool last)
        {
            WritePDU(last);
            //_network.Flush();
        }
        #endregion

        #region Private Members
        private uint CurrentPduSize()
        {
            return _pdu.GetLengthOfPDVs();
        }

        private bool CreatePDV(bool isLast)
        {
            uint len = Math.Min(GetBufferLength(), _max - (CurrentPduSize() + 6));

            //Platform.Log(LogLevel.Info, "Created PDV of length: {0}",len);
            if (_bytes == null || _bytes.Length != len || _pdu.PDVs.Count > 0)
            {
                _bytes = new byte[len];
            }
            _buffer.Read(_bytes, 0, (int)len);

            PDV pdv = new PDV(_pcid, _bytes, _command, isLast);
            _pdu.PDVs.Add(pdv);

            return pdv.IsLastFragment;
        }

        private void WritePDU(bool last)
        {
            if (_pdu.PDVs.Count == 0 || ((CurrentPduSize() + 6) < _max && GetBufferLength() > 0))
            {
                CreatePDV(last);
            }
            if (_pdu.PDVs.Count > 0)
            {
                if (last)
                {
                    _pdu.PDVs[_pdu.PDVs.Count - 1].IsLastFragment = true;
                }
                RawPDU raw = _pdu.Write();

                _networkBase.EnqueuePdu(raw);
                if (OnTick != null)
                    OnTick();
                _pdu = new PDataTF();
            }
        }

        private void AppendBuffer(byte[] buffer, int offset, int count)
        {
            long pos = _buffer.Position;
            _buffer.Seek(0, SeekOrigin.End);
            _buffer.Write(buffer, offset, count);
            _buffer.Position = pos;
        }

        private uint GetBufferLength()
        {
            return (uint)(_buffer.Length - _buffer.Position);
        }
        #endregion

        #region Stream Members
        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            //_network.Flush();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            AppendBuffer(buffer, offset, count);
            while ((CurrentPduSize() + 6 + GetBufferLength()) > _max)
            {
                WritePDU(false);
            }
        }
        #endregion
    }
}
